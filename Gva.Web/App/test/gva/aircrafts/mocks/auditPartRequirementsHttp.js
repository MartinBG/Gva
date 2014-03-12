/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', '/api/auditPartRequirements',
          function (auditPartRequirements) {
            return [200, auditPartRequirements];
          });

  });
}(angular));
