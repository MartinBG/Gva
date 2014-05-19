/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftDocumentApplications?number',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number) {
            var applications = [];
            angular.forEach(aircraft.aircraftDocumentApplications, function (application) {
              if ($params.number) {
                if (application.part.documentNumber === $params.number) {
                  applications.push(application);
                }
              }
            });
            return [200, applications];
          } else {
            return [200, aircraft.aircraftDocumentApplications];
          }
        })
      .when('GET', '/api/aircrafts/:id/aircraftDocumentApplications/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentApplication = _(aircraft.aircraftDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentApplication) {
            return [200, aircraftDocumentApplication];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentApplications',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentApplication = $jsonData;

          aircraftDocumentApplication.partIndex = aircraft.nextIndex++;

          aircraft.aircraftDocumentApplications.push(aircraftDocumentApplication);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentApplications/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentApplication = _(aircraft.aircraftDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentApplication, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftDocumentApplications/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentApplicationInd = _(aircraft.aircraftDocumentApplications)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftDocumentApplications.splice(aircraftDocumentApplicationInd, 1);

          return [200];
        });
  });
}(angular, _));