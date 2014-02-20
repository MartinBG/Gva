/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').config(function ($provide) {
    $provide.decorator('$http', function ($delegate, $templateCache) {
      var originalGet = $delegate.get;
      $delegate.get = function (url, config) {
        if (config.cache &&
          config.cache === $templateCache &&
          (url.lastIndexOf('common', 0) === 0 ||
          url.lastIndexOf('ems', 0) === 0 ||
          url.lastIndexOf('gva', 0) === 0 ||
          url.lastIndexOf('scaffolding', 0) === 0)
        ) {
          return originalGet.call($delegate, 'app/build/templates/' + url, config);
        } else {
          return originalGet.call($delegate, url, config);
        }
      };

      return $delegate;
    });
  }).run(function ($httpBackend) {
    $httpBackend.whenGET(/^app\/build\/templates.*$/).passThrough();
  });
}(angular));
