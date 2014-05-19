/*global angular, _*/
(function (angular, _) {
  /*jshint newcap: false */
  'use strict';
  function $HttpBackendConfiguratorProvider() {
    this.definitions = [];
  }

  $HttpBackendConfiguratorProvider.prototype.when = function (method, pattern, handler) {
    this.definitions.push({
      method: method,
      pattern: pattern,
      handler: handler
    });
    return this;
  };

  $HttpBackendConfiguratorProvider.prototype.xwhen = function () {
    return this;
  };

  $HttpBackendConfiguratorProvider.prototype.$get = [
    '$urlMatcherFactory',
    '$injector',
    function ($urlMatcherFactory, $injector) {
      return new $HttpBackendConfigurator($injector, this.definitions.map(function (definition) {
        return {
          method: definition.method,
          matcher: $urlMatcherFactory.compile(definition.pattern),
          handler: definition.handler
        };
      }));
    }
  ];

  function $HttpBackendConfigurator($injector, definitions) {
    this.$injector = $injector;
    this.definitions = definitions;
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
    var self = this;

    function respond(definition, method, url, data) {
      var urlSplit = url.split('?'),
        path = urlSplit[0],
        query = self.parseQuery(urlSplit[1]);

      var match = definition.matcher.exec(path, query);
      if (!match) {
        throw new Error('Matcher could not parse the provided url!');
      }

      try {
        return self.$injector.invoke(definition.handler, {}, {
          $params: match,
          $data: data,
          $jsonData: data && JSON.parse(data)
        });
      } catch(e) {
        return [500, e];
      }
    }

    self.definitions.forEach(function (definition) {
      var patternWithQuery = definition.matcher.regexp.source.slice(0, -1) + '(\\?.*)?$';

      $httpBackend
        .when(definition.method, new RegExp(patternWithQuery))
        .respond(_.partial(respond, definition));
    });

    // pass through all other requests
    $httpBackend.when('GET', /^.*$/).passThrough();
    $httpBackend.when('POST', /^.*$/).passThrough();
    $httpBackend.when('PUT', /^.*$/).passThrough();
    $httpBackend.when('DELETE', /^.*$/).passThrough();
    $httpBackend.when('PATCH', /^.*$/).passThrough();
  };

  angular.module('app').provider('$httpBackendConfigurator', $HttpBackendConfiguratorProvider);
}(angular, _));
