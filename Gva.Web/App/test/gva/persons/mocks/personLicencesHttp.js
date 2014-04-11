/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/licences',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personLicences];
        })
      .when('GET', '/api/persons/:id/licences/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var licence = _(person.personLicences)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          return [200, licence];
        })
      .when('POST', '/api/persons/:id/licences',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var licence = $jsonData;

          licence.partIndex = person.nextIndex++;

          person.personLicences.push(licence);

          return [200, { partIndex: licence.partIndex }];
        })
      .when('POST', '/api/persons/:id/licences/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var licence = _(person.personLicences)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(licence, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/licences/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var licenceInd = _(person.personLicences)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personLicences.splice(licenceInd, 1);

          return [200];
        });
  });
}(angular, _));