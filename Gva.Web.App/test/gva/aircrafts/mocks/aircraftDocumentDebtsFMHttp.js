/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/aircrafts/:id/aircraftDocumentDebtsFM',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, aircraft.aircraftDocumentDebtsFM];
        })
      .when('GET', '/api/aircrafts/:id/aircraftDocumentDebtsFM/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = _(aircraft.aircraftDocumentDebtsFM)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentDebt) {
            return [200, aircraftDocumentDebt];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentDebtsFM',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = $jsonData;

          aircraftDocumentDebt.partIndex = aircraft.nextIndex++;

          aircraft.aircraftDocumentDebtsFM.push(aircraftDocumentDebt);

          return [200];
        })
      .when('POST', '/api/aircrafts/:id/aircraftDocumentDebtsFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebt = _(aircraft.aircraftDocumentDebtsFM)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentDebt, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/aircrafts/:id/aircraftDocumentDebtsFM/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentDebtInd = _(aircraft.aircraftDocumentDebtsFM)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftDocumentDebtsFM.splice(aircraftDocumentDebtInd, 1);

          return [200];
        });
  });
}(angular, _));