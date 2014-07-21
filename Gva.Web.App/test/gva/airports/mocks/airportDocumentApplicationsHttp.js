/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/airports/:id/airportDocumentApplications?number',
        function ($params, $filter, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number) {
            var applications = [];
            angular.forEach(airport.airportDocumentApplications, function (application) {
              if ($params.number) {
                if (application.part.documentNumber === $params.number) {
                  applications.push(application);
                }
              }
            });
            return [200, applications];
          } else {
            return [200, airport.airportDocumentApplications];
          }
        })
      .when('GET', 'api/airports/:id/airportDocumentApplications/:ind',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentApplication = _(airport.airportDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (airportDocumentApplication) {
            return [200, airportDocumentApplication];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/airports/:id/airportDocumentApplications',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentApplication = $jsonData;

          airportDocumentApplication.partIndex = airport.nextIndex++;

          airport.airportDocumentApplications.push(airportDocumentApplication);

          return [200];
        })
      .when('POST', 'api/airports/:id/airportDocumentApplications/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentApplication = _(airport.airportDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airportDocumentApplication, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/airports/:id/airportDocumentApplications/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentApplicationInd = _(airport.airportDocumentApplications)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          airport.airportDocumentApplications.splice(airportDocumentApplicationInd, 1);

          return [200];
        });
  });
}(angular, _));