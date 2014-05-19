/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationDocumentApplications?number',
        function ($params, $filter, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number) {
            var applications = [];
            angular.forEach(organization.organizationDocumentApplications, function (application) {
              if ($params.number) {
                if (application.part.documentNumber === $params.number) {
                  applications.push(application);
                }
              }
            });
            return [200, applications];
          } else {
            return [200, organization.organizationDocumentApplications];
          }
        })
      .when('GET', '/api/organizations/:id/organizationDocumentApplications/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentApplication = _(organization.organizationDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationDocumentApplication) {
            return [200, organizationDocumentApplication];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/organizationDocumentApplications',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentApplication = $jsonData;

          organizationDocumentApplication.partIndex = organization.nextIndex++;

          organization.organizationDocumentApplications.push(organizationDocumentApplication);

          return [200];
        })
      .when('POST', '/api/organizations/:id/organizationDocumentApplications/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentApplication = _(organization.organizationDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationDocumentApplication, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/organizationDocumentApplications/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationDocumentApplicationInd = _(organization.organizationDocumentApplications)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.organizationDocumentApplications
            .splice(organizationDocumentApplicationInd, 1);

          return [200];
        });
  });
}(angular, _));