/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/organizations/:id/organizationRegGroundServiceOperators',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.organizationRegGroundServiceOperators];
        })
      .when('GET', 'api/organizations/:id/organizationRegGroundServiceOperators/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRegGroundServiceOperator =
            _(organization.organizationRegGroundServiceOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationRegGroundServiceOperator) {
            return [200, organizationRegGroundServiceOperator];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations/:id/organizationRegGroundServiceOperators',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRegGroundServiceOperator = $jsonData;

          organizationRegGroundServiceOperator.partIndex = organization.nextIndex++;

          organization.organizationRegGroundServiceOperators
            .push(organizationRegGroundServiceOperator);

          return [200];
        })
      .when('POST', 'api/organizations/:id/organizationRegGroundServiceOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRegGroundServiceOperator =
            _(organization.organizationRegGroundServiceOperators)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationRegGroundServiceOperator, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/organizations/:id/organizationRegGroundServiceOperators/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var regGroundServiceOperatorInd = _(organization.organizationRegGroundServiceOperators)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.organizationRegGroundServiceOperators.splice(regGroundServiceOperatorInd, 1);

          return [200];
        });
  });
}(angular, _));