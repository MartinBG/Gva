/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/airports/:id/airportDocumentOwners',
        function ($params, $filter, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, airport.airportDocumentOwners];
        })
      .when('GET', 'api/airports/:id/airportDocumentOwners/:ind',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOwner = _(airport.airportDocumentOwners)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (airportDocumentOwner) {
            return [200, airportDocumentOwner];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/airports/:id/airportDocumentOwners',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOwner = $jsonData;

          airportDocumentOwner.partIndex = airport.nextIndex++;

          airport.airportDocumentOwners.push(airportDocumentOwner);

          return [200];
        })
      .when('POST', 'api/airports/:id/airportDocumentOwners/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOwner = _(airport.airportDocumentOwners)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airportDocumentOwner, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/airports/:id/airportDocumentOwners/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportDocumentOwnerInd = _(airport.airportDocumentOwners)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          airport.airportDocumentOwners.splice(airportDocumentOwnerInd, 1);

          return [200];
        });
  });
}(angular, _));