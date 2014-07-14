/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('auth', ['ng']).constant('authServiceConfig', {
    tokenUrl: '/api/token'
  }).factory('sessionTokenStore', ['$window', function ($window) {
    var cookies, sessionKey;

    function readCookie(name){
      var all,
          cookie,
          i;

      if (cookies) {
        return cookies[name];
      }

      all = $window.document.cookie.split('; ');
      cookies = {};

      for (i = 0; i < all.length; i++) {
        cookie = all[i].split('=');
        cookies[cookie[0]] = cookie[1];
      }

      return cookies[name];
    }

    sessionKey = readCookie('sessionCookie');

    function SessionTokenStore() {
      if ($window.localStorage.sessionTokens) {
        this.token = JSON.parse($window.localStorage.sessionTokens)[sessionKey];
      }
    }

    SessionTokenStore.prototype.getToken = function () {
      if (this.token) {
        return this.token.token;
      }

      return null;
    };

    SessionTokenStore.prototype.setToken = function (token) {
      var now = new Date(),
          tokens;

      if ($window.localStorage.sessionTokens) {
        tokens = JSON.parse($window.localStorage.sessionTokens);
      }

      tokens = tokens || {};

      this.token = {
        token: token,
        addedOn: now
      };
      tokens[sessionKey] = this.token;

      //remove tokens older than a week
      _.each(tokens, function (token, sessionKey) {
        if ((now - token.addedOn) > 7 * 24 * 3600 * 1000) {
          delete tokens[sessionKey];
        }
      });

      $window.localStorage.sessionTokens = JSON.stringify(tokens);
    };

    SessionTokenStore.prototype.deleteToken = function () {
      var tokens;

      if ($window.localStorage.sessionTokens) {
        tokens = JSON.parse($window.localStorage.sessionTokens);
        delete tokens[sessionKey];
        $window.localStorage.sessionTokens = JSON.stringify(tokens);
      }
    };

    return new SessionTokenStore();
  }]).factory('authService', [
    '$q',
    '$injector',
    '$rootScope',
    'authServiceConfig',
    'sessionTokenStore',
    function ($q, $injector, $rootScope, authServiceConfig, sessionTokenStore) {
      var $http;

      function getHttp() {
        //service initialized later because of circular dependency problem.
        $http = $http || $injector.get('$http');
      }

      function retryHttpRequest(config, deferred) {
        function successCallback(response) {
          deferred.resolve(response);
        }
        function errorCallback(response) {
          deferred.reject(response);
        }
        getHttp();
        $http(config).then(successCallback, errorCallback);
      }

      function createFormUrlEncodedString(data) {
        var res = [];
        _.forOwn(data, function (value, key) {
          res.push(encodeURIComponent(key) + '=' + encodeURIComponent(value));
        });
        return res.join('&');
      }

      function AuthService() {
        this.failedRequests = [];
      }

      AuthService.prototype.authenticate = function(username, password) {
        var self = this;
        getHttp();

        return $http({
          method: 'POST',
          url: authServiceConfig.tokenUrl,
          headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'},
          data: createFormUrlEncodedString({
            username: username,
            password: password,
            grant_type: 'password'
          })
        }).then(function (response) {
          if (response.data.token_type !== 'bearer' &&
              !response.data.access_token) {
            return $q.reject('Unsupported token type');
          }

          sessionTokenStore.setToken(response.data.access_token);
          self.retryFailedRequests();

          return true;
        }, function (response) {
          if (response.data.error === 'invalid_grant') {
            return false;
          }
          return $q.reject(response.data);
        });
      };

      AuthService.prototype.authConfig = function (config) {
        var accessToken = sessionTokenStore.getToken();
        if (accessToken) {
          config.headers.Authorization = 'Bearer ' + accessToken;
        }

        return config;
      };

      AuthService.prototype.addFailedRequest = function(config) {
        var deferred = $q.defer();
        this.failedRequests.push({
          config: config,
          deferred: deferred
        });

        $rootScope.$broadcast('authRequired', this);
        return deferred.promise;
      };

      AuthService.prototype.retryFailedRequests = function() {
        var self = this;
        _.forEach(this.failedRequests, function (req) {
          retryHttpRequest(self.authConfig(req.config), req.deferred);
        });
        this.failedRequests = [];
      };

      AuthService.prototype.signOut = function() {
        sessionTokenStore.deleteToken();
        $rootScope.$broadcast('authRequired', this);

        //return a resolved promise
        return $q.when();
      };

      return new AuthService();
    }
  ]).factory('authHttpInterceptor', ['$q', 'authService', function ($q, authService) {
    return {
      request: function(config) {
        return authService.authConfig(config);
      },
      responseError: function(rejection) {
        if (rejection.status === 401) {
          return authService.addFailedRequest(rejection.config);
        }
        // otherwise, default behaviour
        return $q.reject(rejection);
      }
    };
  }]).config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('authHttpInterceptor');
  }]);
}(angular, _));
