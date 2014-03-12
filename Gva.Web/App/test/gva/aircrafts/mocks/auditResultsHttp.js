/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', '/api/auditResults?alias',
          function ($params, auditResults) {
            return [200, _.where(auditResults, { alias: $params.alias })[0]];
          });

  });
}(angular, _));
