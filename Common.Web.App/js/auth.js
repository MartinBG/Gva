/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('auth', ['ng']).constant('authServiceConfig', {
    tokenUrl: '/api/token'
  }).factory('authService', [
    '$q',
    '$injector',
    '$rootScope',
    '$window',
    '$timeout',
    'authServiceConfig',
    function ($q, $injector, $rootScope, $window, $timeout, authServiceConfig) {
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
        getHttp();

        delete $window.localStorage.token;
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
          var expiresIn;
          if (response.data.token_type !== 'bearer' &&
              !response.data.access_token) {
            return $q.reject('Unsupported token type');
          }

          expiresIn = response.data.expires_in && parseInt(response.data.expires_in, 10);
          if (expiresIn) {
            $timeout(function () {
              delete $window.localStorage.token;
              $rootScope.$broadcast('authRequired', this);
            }, expiresIn * 1000);
          }

          $window.localStorage.token = response.data.access_token;
          return true;
        }, function (response) {
          if (response.data.error === 'invalid_grant') {
            return false;
          }
          return $q.reject(response.data);
        });
      };

      AuthService.prototype.authConfig = function (config) {
        if ($window.localStorage.token) {
          config.headers.Authorization = 'Bearer ' + $window.localStorage.token;
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
