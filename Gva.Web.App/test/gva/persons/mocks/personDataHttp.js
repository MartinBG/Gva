/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/persons/:id/personData',
        function ($params, personLots) {
          var personLot = _(personLots).filter({ lotId: parseInt($params.id, 10) }).first();

          if (personLot) {
            return [200, personLot.personData];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/persons/:id/personData',
        function ($params, $jsonData, personLots) {
          var personLot = _(personLots).filter({ lotId: parseInt($params.id, 10) }).first();

          personLot.personData = $jsonData;

          if (personLot) {
            return [200];
          }
          else {
            return [404];
          }
        });
  });
}(angular, _));
