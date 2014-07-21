/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/organizations/:id/organizationData',
        function ($params, organizationLots) {
          var organizationLot = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if (organizationLot) {
            return [200, organizationLot.organizationData];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations/:id/organizationData',
        function ($params, $jsonData, organizationLots) {
          var organizationLot = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          organizationLot.organizationData = $jsonData;

          if (organizationLot) {
            return [200];
          }
          else {
            return [404];
          }
        });
  });
}(angular, _));
