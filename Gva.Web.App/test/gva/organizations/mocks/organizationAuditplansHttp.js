/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/organizations/:id/organizationAuditplans',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.organizationAuditplans];
        })
      .when('GET', 'api/organizations/:id/organizationAuditplans/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAuditplan = _(organization.organizationAuditplans)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationAuditplan) {
            return [200, organizationAuditplan];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations/:id/organizationAuditplans',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAuditplan = $jsonData;

          organizationAuditplan.partIndex = organization.nextIndex++;

          organization.organizationAuditplans.push(organizationAuditplan);

          return [200];
        })
      .when('POST', 'api/organizations/:id/organizationAuditplans/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAuditplan = _(organization.organizationAuditplans)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationAuditplan, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/organizations/:id/organizationAuditplans/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAuditplanInd = _(organization.organizationAuditplans)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.organizationAuditplans.splice(organizationAuditplanInd, 1);

          return [200];
        });
  });
}(angular, _));