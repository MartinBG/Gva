/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when(
      'GET',
      'api/organizations/:id/organizationDocumentOthers?number&typeid&publ&datef&pnumber',
        function ($params, $filter, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.organizationDocumentOthers];
        })
      .when('GET', 'api/organizations/:id/organizationDocumentOthers/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentOther = _(organization.organizationDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationDocumentOther) {
            return [200, organizationDocumentOther];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/organizations/:id/organizationDocumentOthers',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentOther = $jsonData;

          organizationDocumentOther.partIndex = organization.nextIndex++;

          organization.organizationDocumentOthers.push(organizationDocumentOther);

          return [200];
        })
      .when('POST', 'api/organizations/:id/organizationDocumentOthers/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentOther = _(organization.organizationDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationDocumentOther, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/organizations/:id/organizationDocumentOthers/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentOtherInd = _(organization.organizationDocumentOthers)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.organizationDocumentOthers.splice(organizationDocumentOtherInd, 1);

          return [200];
        });
  });
}(angular, _));