/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', 'api/airports/:id/inspections',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          return [200, airport.airportInspections];
        })
      .when('GET', 'api/airports/:id/inspections/:ind',
        function ($params, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var airportInspection = _(airport.airportInspections)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (airportInspection) {
            return [200, airportInspection];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/airports/:id/inspections',
            function ($params, $jsonData, airportLots) {
              var airport = _(airportLots)
                .filter({ lotId: parseInt($params.id, 10) }).first();

              var airportInspection = $jsonData;

              airportInspection.partIndex = airport.nextIndex++;

              airport.airportInspections.push(airportInspection);

              return [200];
            })
      .when('POST', 'api/airports/:id/inspections/:ind',
        function ($params, $jsonData, airportLots) {
          var airport = _(airportLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var airportInspection = _(airport.airportInspections)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(airportInspection, $jsonData);

          return [200];
        });
  });
}(angular, _));