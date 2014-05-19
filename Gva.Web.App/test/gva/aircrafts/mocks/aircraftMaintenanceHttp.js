/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/maintenances',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, aircraft.aircraftMaintenances];
        })
      .when('GET', '/api/aircrafts/:id/maintenances/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftMaintenance = _(aircraft.aircraftMaintenances)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftMaintenance) {
            return [200, aircraftMaintenance];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/maintenances',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftMaintenance = $jsonData;

          aircraftMaintenance.partIndex = aircraft.nextIndex++;

          aircraft.aircraftMaintenances.push(aircraftMaintenance);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/maintenances/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftMaintenance = _(aircraft.aircraftMaintenances)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftMaintenance, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/maintenances/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftMaintenanceInd = _(aircraft.aircraftMaintenances)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftMaintenances.splice(aircraftMaintenanceInd, 1);

          return [200];
        });
  });
}(angular, _));