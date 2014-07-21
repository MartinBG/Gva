/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
        .when('GET', 'api/limitation66Types',
          function ($params, $filter, limitation66Types) {
            return [200, limitation66Types];
          });

  });
}(angular));
