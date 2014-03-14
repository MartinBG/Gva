/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/staffManagement',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.staffManagement];
        })
      .when('GET', '/api/organizations/:id/staffManagement/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffManagement = _(organization.staffManagement)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (staffManagement) {
            return [200, staffManagement];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/staffManagement',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffManagement = $jsonData;

          staffManagement.partIndex = organization.nextIndex++;

          organization.staffManagement.push(staffManagement);

          return [200];
        })
      .when('POST', '/api/organizations/:id/staffManagement/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffManagement = _(organization.staffManagement)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(staffManagement, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/staffManagement/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var staffManagementInd = _(organization.staffManagement)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.staffManagement.splice(staffManagementInd, 1);

          return [200];
        });
  });
}(angular, _));