/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/aircrafts/:id/aircraftData',
        function ($params, aircraftLots) {
          var aircraftLot = _(aircraftLots).filter({ lotId: parseInt($params.id, 10) }).first();

          if (aircraftLot) {
            return [200, aircraftLot.aircraftData];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/aircrafts/:id/aircraftData',
        function ($params, $jsonData, aircraftLots) {
          var aircraftLot = _(aircraftLots).filter({ lotId: parseInt($params.id, 10) }).first();

          aircraftLot.aircraftData = $jsonData;

          if (aircraftLot) {
            return [200];
          }
          else {
            return [404];
          }
        });
  });
}(angular, _));
