/*global angular,_*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($provide) {
    $provide.decorator('$httpBackend', angular.mock.e2e.$httpBackendDecorator);

    $provide.decorator('$httpBackend', function ($delegate, $delay) {
      var proxy = function (method, url, data, callback, headers, timeout, withCredentials) {
        var promiseAwareCallback = function (status, data, headers) {
          var self = this;

          // wrap the result if not a promise
          if (!(data && data.then && typeof (data.then) === 'function')) {
            data = $delay(500, data);
          }

          data.then(function (result) {
            result = _.cloneDeep(result);

            callback.call(self, status, result, headers);
          });
        };

        data = _.cloneDeep(data);

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
