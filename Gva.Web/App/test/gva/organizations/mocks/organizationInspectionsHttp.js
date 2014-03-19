/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/inspections',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          return [200, organization.organizationInspections];
        })
      .when('GET', '/api/organizations/:id/inspections/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var organizationInspection = _(organization.organizationInspections)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationInspection) {
            return [200, organizationInspection];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/inspections',
            function ($params, $jsonData, organizationLots) {
              var organization = _(organizationLots)
                .filter({ lotId: parseInt($params.id, 10) }).first();

              var organizationInspection = $jsonData;

              organizationInspection.partIndex = organization.nextIndex++;

              organization.organizationInspections.push(organizationInspection);

              return [200];
            })
      .when('POST', '/api/organizations/:id/inspections/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationInspection = _(organization.organizationInspections)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationInspection, $jsonData);

          return [200];
        });
  });
}(angular, _));