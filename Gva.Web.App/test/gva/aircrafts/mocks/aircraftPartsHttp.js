/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/aircraftParts',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, aircraft.aircraftParts];
        })
      .when('GET', 'api/aircrafts/:id/aircraftParts/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftPart = _(aircraft.aircraftParts)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftPart) {
            return [200, aircraftPart];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftParts',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftPart = $jsonData;

          aircraftPart.partIndex = aircraft.nextIndex++;

          aircraft.aircraftParts.push(aircraftPart);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftParts/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftPart = _(aircraft.aircraftParts)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftPart, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftParts/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftPartInd = _(aircraft.aircraftParts)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftParts.splice(aircraftPartInd, 1);

          return [200];
        });
  });
}(angular, _));