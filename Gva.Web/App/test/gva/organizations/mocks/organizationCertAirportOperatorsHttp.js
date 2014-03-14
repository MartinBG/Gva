/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/certAirportOperators',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.certAirportOperators];
        })
      .when('GET', '/api/organizations/:id/certAirportOperators/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certAirportOperator = _(organization.certAirportOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (certAirportOperator) {
            return [200, certAirportOperator];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/certAirportOperators',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certAirportOperator = $jsonData;

          certAirportOperator.partIndex = organization.nextIndex++;

          organization.certAirportOperators.push(certAirportOperator);

          return [200];
        })
      .when('POST', '/api/organizations/:id/certAirportOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certAirportOperator = _(organization.certAirportOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(certAirportOperator, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/certAirportOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var certAirportOperatorInd = _(organization.certAirportOperators)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.certAirportOperators.splice(certAirportOperatorInd, 1);

          return [200];
        });
  });
}(angular, _));