/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/organizations/:id/organizationRecommendations',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, organization.recommendations];
        })
      .when('GET', '/api/organizations/:id/organizationRecommendations/:ind',
        function ($params, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRecommendation = _(organization.recommendations)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (organizationRecommendation) {
            return [200, organizationRecommendation];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/organizations/:id/organizationRecommendations',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRecommendation = $jsonData;

          organizationRecommendation.partIndex = organization.nextIndex++;

          organization.recommendations.push(organizationRecommendation);

          return [200];
        })
      .when('POST', '/api/organizations/:id/organizationRecommendations/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRecommendation = _(organization.recommendations)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(organizationRecommendation, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/organizations/:id/organizationRecommendations/:ind',
        function ($params, $jsonData, organizationLots) {
          var organization = _(organizationLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var organizationRecommendationInd = _(organization.recommendations)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          organization.recommendations.splice(organizationRecommendationInd, 1);

          return [200];
        });
  });
}(angular, _));