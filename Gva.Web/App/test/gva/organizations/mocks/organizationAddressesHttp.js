/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationAddresses',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.organizationAddresses];
        })
      .when('GET', '/api/organizations/:id/organizationAddresses/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAddress = _(organization.organizationAddresses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationAddress) {
            return [200, organizationAddress];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/organizationAddresses',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAddress = $jsonData;

          organizationAddress.partIndex = organization.nextIndex++;

          organization.organizationAddresses.push(organizationAddress);

          return [200];
        })
      .when('POST', '/api/organizations/:id/organizationAddresses/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAddress = _(organization.organizationAddresses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationAddress, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/organizationAddresses/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationAddressInd = _(organization.organizationAddresses)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.organizationAddresses.splice(organizationAddressInd, 1);

          return [200];
        });
  });
}(angular, _));