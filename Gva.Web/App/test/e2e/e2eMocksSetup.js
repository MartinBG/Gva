/*global angular,_*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($provide) {
    $provide.decorator('$httpBackend', angular.mock.e2e.$httpBackendDecorator);

    $provide.decorator('$httpBackend', function ($delegate) {
      var proxy = function (method, url, data, callback, headers, timeout, withCredentials) {
        var promiseAwareCallback = function (status, data, headers) {
          var self = this;

          // check if the result is promise
          if (data && data.then && typeof (data.then) === 'function') {
            data.then(function (result) {
              callback.call(self, status, result, headers);
            });
          } else {
            callback.call(self, status, data, headers);
          }
        };

        return $delegate.call(this,
          method, url, data, promiseAwareCallback, headers, timeout, withCredentials);
      };

      _.forOwn($delegate, function (value, key) {
        proxy[key] = value;
      });

      return proxy;
    });
  });
  
  angular.module('app').run(function ($httpBackend, $httpBackendConfigurator) {
    $httpBackendConfigurator.configure($httpBackend);
  });
}(angular, _));
