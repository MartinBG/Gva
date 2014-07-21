/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/documentOccurrences',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          return [200, aircraft.aircraftDocumentOccurrences];
        })
      .when('GET', 'api/aircrafts/:id/documentOccurrences/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var aircraftDocumentOccurrence = _(aircraft.aircraftDocumentOccurrences)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentOccurrence) {
            return [200, aircraftDocumentOccurrence];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/documentOccurrences',
            function ($params, $jsonData, aircraftLots) {
              var aircraft = _(aircraftLots)
                .filter({ lotId: parseInt($params.id, 10) }).first();

              var aircraftDocumentOccurrence = $jsonData;

              aircraftDocumentOccurrence.partIndex = aircraft.nextIndex++;

              aircraft.aircraftDocumentOccurrences.push(aircraftDocumentOccurrence);

              return [200, aircraft];
            })
      .when('POST', 'api/aircrafts/:id/documentOccurrences/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOccurrence = _(aircraft.aircraftDocumentOccurrences)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentOccurrence, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/documentOccurrences/:ind',
            function ($params, $jsonData, aircraftLots) {
              var aircraft = _(aircraftLots)
                .filter({ lotId: parseInt($params.id, 10) }).first();

              var aircraftDocumentOccurrenceInd = _(aircraft.aircraftDocumentOccurrences)
                .findIndex({ partIndex: parseInt($params.ind, 10) });

              aircraft.aircraftDocumentOccurrences.splice(aircraftDocumentOccurrenceInd, 1);

              return [200];
            });
  });
}(angular, _));