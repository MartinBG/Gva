/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/aircraftDocumentOwners',
        function ($params, $filter, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, aircraft.aircraftDocumentOwners];
        })
      .when('GET', 'api/aircrafts/:id/aircraftDocumentOwners/:ind',
        function ($params, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOwner = _(aircraft.aircraftDocumentOwners)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (aircraftDocumentOwner) {
            return [200, aircraftDocumentOwner];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftDocumentOwners',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOwner = $jsonData;

          aircraftDocumentOwner.partIndex = aircraft.nextIndex++;

          aircraft.aircraftDocumentOwners.push(aircraftDocumentOwner);

          return [200];
        })
      .when('POST', 'api/aircrafts/:id/aircraftDocumentOwners/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOwner = _(aircraft.aircraftDocumentOwners)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(aircraftDocumentOwner, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/aircrafts/:id/aircraftDocumentOwners/:ind',
        function ($params, $jsonData, aircraftLots) {
          var aircraft = _(aircraftLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var aircraftDocumentOwnerInd = _(aircraft.aircraftDocumentOwners)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          aircraft.aircraftDocumentOwners.splice(aircraftDocumentOwnerInd, 1);

          return [200];
        });
  });
}(angular, _));