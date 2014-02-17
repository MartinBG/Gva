/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').config(function ($provide) {
    $provide.decorator('$http', function ($delegate, $templateCache) {
      var originalGet = $delegate.get;
      $delegate.get = function (url, config) {
        if (config.cache && config.cache === $templateCache) {
          return originalGet.call($delegate, 'app/build/templates/js/' + url, config);
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
