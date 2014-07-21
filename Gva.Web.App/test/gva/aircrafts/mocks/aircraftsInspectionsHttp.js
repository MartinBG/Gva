/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/inspections',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          return [200, aircraft.aircraftInspections];
        })
      .when('GET', 'api/aircrafts/:id/inspections/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var aircraftInspection = _(aircraft.aircraftInspections)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftInspection) {
            return [200, aircraftInspection];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/inspections',
            function ($params, $jsonData, aircraftLots) {
              var aircraft = _(aircraftLots)
                .filter({ lotId: parseInt($params.id, 10) }).first();

              var aircraftInspection = $jsonData;

              aircraftInspection.partIndex = aircraft.nextIndex++;

              aircraft.aircraftInspections.push(aircraftInspection);

              return [200];
            })
      .when('POST', 'api/aircrafts/:id/inspections/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftInspection = _(aircraft.aircraftInspections)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftInspection, $jsonData);

          return [200];
        });
  });
}(angular, _));