/*global angular*/
(function (angular) {
  /*jshint newcap: false */
  'use strict';
  function $HttpBackendConfiguratorProvider() {
    this.patterns = [];
  }

  $HttpBackendConfiguratorProvider.prototype.when = function (method, pattern, handler) {
    this.patterns.push({
      method: method,
      pattern: pattern,
      handler: handler
    });
    return this;
  };

  $HttpBackendConfiguratorProvider.prototype.$get = [
    '$urlMatcherFactory',
    '$injector',
    function ($urlMatcherFactory, $injector) {
      return new $HttpBackendConfigurator($injector, this.patterns.map(function (pattern) {
        return {
          method: pattern.method,
          matcher: $urlMatcherFactory.compile(pattern.pattern),
          handler: pattern.handler
        };
      }));
    }
  ];

  function $HttpBackendConfigurator($injector, matchers) {
    this.$injector = $injector;
    this.matchersPerMethod = matchers.reduce(function (res, matcher) {
      if (!res[matcher.method]) {
        res[matcher.method] = [];
      }
      res[matcher.method].push(matcher);
      return res;
    }, {});
  }

  $HttpBackendConfigurator.prototype.parseQuery = function (query) {
    var queryString = query && query[0] === '?' ? query.substring(1) : (query || '');

    return queryString
      .split('&')
      .reduce(function (res, part) {
        var splitPart;
        if (part) {
          splitPart = part.split('=');
          res[splitPart[0]] = decodeURIComponent(splitPart[1].replace('+', '%20'));
        }
        return res;
      }, {});
  };

  $HttpBackendConfigurator.prototype.configure = function ($httpBackend) {
    var self = this,
      method;

    function respond(method, url, data) {
      var matchers = self.matchersPerMethod[method],
        urlSplit = url.split('?'),
        path = urlSplit[0],
        query = self.parseQuery(urlSplit[1]),
        i;

      for (i = 0; i < matchers.length; i++) {
        var match = matchers[i].matcher.exec(path, query);
        if (match) {
          return self.$injector.invoke(matchers[i].handler, {}, {
            $params: match,
            $data: data,
            $jsonData: data && JSON.parse(data)
          });
        }
      }
    }

    for (method in self.matchersPerMethod) {
      if (self.matchersPerMethod.hasOwnProperty(method)) {
        $httpBackend.when(method, /^.*$/).respond(respond);
      }
    }
  };

  angular.module('app').provider('$httpBackendConfigurator', $HttpBackendConfiguratorProvider);
}(angular));
