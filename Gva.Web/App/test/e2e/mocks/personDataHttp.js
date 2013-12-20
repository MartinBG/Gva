/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personData',
        function ($params, $delay, personLots) {
          var personLot = _(personLots).filter({ lotId: parseInt($params.id, 10) }).first();

          if (personLot) {
            return [200, $delay(500, personLot.personData)];
          }
          else {
            return [404, $delay(500)];
          }
        })
      .when('POST', '/api/persons/:id/personData',
        function ($params, $jsonData, $delay, personLots) {
          var personLot = _(personLots).filter({ lotId: parseInt($params.id, 10) }).first();

          personLot.personData = $jsonData;

          if (personLot) {
            return [200, $delay(500)];
          }
          else {
            return [404, $delay(500)];
          }
        });
  });
}(angular, _));
