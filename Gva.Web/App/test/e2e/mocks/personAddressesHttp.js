/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personAddresses',
        function ($params, $delay, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, $delay(500, person.personAddresses)];
        })
      .when('GET', '/api/persons/:id/personAddresses/:ind',
        function ($params, $delay, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personAddress = _(person.personAddresses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personAddress) {
            return [200, $delay(500, personAddress)];
          }
          else {
            return [404, $delay(500)];
          }
        })
      .when('POST', '/api/persons/:id/personAddresses',
        function ($params, $jsonData, $delay, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personAddress = $jsonData;

          personAddress.partIndex = person.nextIndex++;

          person.personAddresses.push(personAddress);

          return [200, $delay(500)];
        })
      .when('POST', '/api/persons/:id/personAddresses/:ind',
        function ($params, $jsonData, $delay, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personAddress = _(person.personAddresses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personAddress, $jsonData);

          return [200, $delay(500)];
        })
      .when('DELETE', '/api/persons/:id/personAddresses/:ind',
        function ($params, $jsonData, $delay, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personAddressInd = _(person.personAddresses)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personAddresses.splice(personAddressInd, 1);

          return [200, $delay(500)];
        });
  });
}(angular, _));