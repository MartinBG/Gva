(function (angular) {
  'use strict';

  angular.module('app').config(function ($provide) {
    $provide.decorator('$httpBackend', angular.mock.e2e.$httpBackendDecorator);
  });
  
  angular.module('app').run(function ($httpBackend, $httpBackendConfigurator) {
    $httpBackendConfigurator.configure($httpBackend);
  });
}(angular));
