/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/persons/:id/personStatuses',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personStatuses];
        })
      .when('GET', 'api/persons/:id/personStatuses/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personStatus = _(person.personStatuses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personStatus) {
            return [200, personStatus];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/persons/:id/personStatuses',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personStatus = $jsonData;

          personStatus.partIndex = person.nextIndex++;

          person.personStatuses.push(personStatus);

          return [200];
        })
      .when('POST', 'api/persons/:id/personStatuses/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personStatus = _(person.personStatuses)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personStatus, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/persons/:id/personStatuses/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personStatusInd = _(person.personStatuses)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personStatuses.splice(personStatusInd, 1);

          return [200];
        });
  });
}(angular, _));
