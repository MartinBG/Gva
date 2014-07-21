/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/aircraftDocumentDebts',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, aircraft.aircraftDocumentDebts];
        })
      .when('GET', 'api/aircrafts/:id/aircraftDocumentDebts/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = _(aircraft.aircraftDocumentDebts)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentDebt) {
            return [200, aircraftDocumentDebt];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftDocumentDebts',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = $jsonData;

          aircraftDocumentDebt.partIndex = aircraft.nextIndex++;

          aircraft.aircraftDocumentDebts.push(aircraftDocumentDebt);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftDocumentDebts/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = _(aircraft.aircraftDocumentDebts)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentDebt, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftDocumentDebts/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebtInd = _(aircraft.aircraftDocumentDebts)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftDocumentDebts.splice(aircraftDocumentDebtInd, 1);

          return [200];
        });
  });
}(angular, _));