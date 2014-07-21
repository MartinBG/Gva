/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/organizations/:id/organizationCertGroundServiceOperators',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.certGroundServiceOperators];
        })
      .when('GET', 'api/organizations/:id/organizationCertGroundServiceOperators/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certGroundServiceOperator = _(organization.certGroundServiceOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (certGroundServiceOperator) {
            return [200, certGroundServiceOperator];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations/:id/organizationCertGroundServiceOperators',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certGroundServiceOperator = $jsonData;

          certGroundServiceOperator.partIndex = organization.nextIndex++;

          organization.certGroundServiceOperators.push(certGroundServiceOperator);

          return [200];
        })
      .when('POST', 'api/organizations/:id/organizationCertGroundServiceOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certGroundServiceOperator = _(organization.certGroundServiceOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(certGroundServiceOperator, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/organizations/:id/organizationCertGroundServiceOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certGroundServiceOperatorInd = _(organization.certGroundServiceOperators)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.certGroundServiceOperators.splice(certGroundServiceOperatorInd, 1);

          return [200];
        });
  });
}(angular, _));