/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/airports/:id/airportData',
        function ($params, airportLots) {
          var airportLot = _(airportLots).filter({ lotId: parseInt($params.id, 10) }).first();

          if (airportLot) {
            return [200, airportLot.airportData];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/airports/:id/airportData',
        function ($params, $jsonData, airportLots) {
          var airportLot = _(airportLots).filter({ lotId: parseInt($params.id, 10) }).first();

          airportLot.airportData = $jsonData;

          if (airportLot) {
            return [200];
          }
          else {
            return [404];
          }
        });
  });
}(angular, _));
