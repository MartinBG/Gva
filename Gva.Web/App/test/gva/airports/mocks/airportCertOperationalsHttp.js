/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/airports/:id/airportCertOperationals',
        function ($params, $filter, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, airport.airportCertOperationals];
        })
      .when('GET', '/api/airports/:id/airportCertOperationals/:ind',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportCertOperational = _(airport.airportCertOperationals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (airportCertOperational) {
            return [200, airportCertOperational];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/airports/:id/airportCertOperationals',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportCertOperational = $jsonData;

          airportCertOperational.partIndex = airport.nextIndex++;

          airport.airportCertOperationals.push(airportCertOperational);

          return [200];
        })
      .when('POST', '/api/airports/:id/airportCertOperationals/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportCertOperational = _(airport.airportCertOperationals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airportCertOperational, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/airports/:id/airportCertOperationals/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportCertOperationalInd = _(airport.airportCertOperationals)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          airport.airportCertOperationals.splice(airportCertOperationalInd, 1);

          return [200];
        });
  });
}(angular, _));