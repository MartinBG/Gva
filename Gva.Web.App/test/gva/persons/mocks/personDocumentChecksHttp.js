/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentChecks?number&typeid&publ&datef&pnumber',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.pnumber ||
            $params.number ||
            $params.typeid ||
            $params.publ ||
            $params.datef) {
            var checks = [],
                exists;
            angular.forEach(person.personDocumentChecks, function (check) {
              if ($params.number) {
                exists = check.part.documentNumber === $params.number;
              } else if ($params.pnumber) {
                exists = check.part.documentPersonNumber === $params.pnumber;
              } else if ($params.typeid) {
                var typeId = parseInt($params.typeid, 10);
                exists = check.part.personCheckDocumentType.nomValueId === typeId;
              } else if ($params.publ) {
                exists = check.part.documentPublisher === $params.publ;
              } else if ($params.datef) {
                var newDate = $filter('date')($params.datef, 'mediumDate'),
                  oldDate = $filter('date')(check.part.documentDateValidFrom, 'mediumDate');
                exists = newDate === oldDate;
              }
              if (exists) {
                checks.push(check);
              }
            });
            return [200, checks];
          } else {
            return [200, person.personDocumentChecks];
          }
        })
      .when('GET', '/api/persons/:id/personDocumentChecks/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentCheck = _(person.personDocumentChecks)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentCheck) {
            return [200, personDocumentCheck];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentChecks',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentCheck = $jsonData;

          personDocumentCheck.partIndex = person.nextIndex++;

          person.personDocumentChecks.push(personDocumentCheck);

          return [200];
        })
      .when('POST', '/api/persons/:id/personDocumentChecks/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentCheck = _(person.personDocumentChecks)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentCheck, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/personDocumentChecks/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentCheckInd = _(person.personDocumentChecks)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personDocumentChecks.splice(personDocumentCheckInd, 1);

          return [200];
        });
  });
}(angular, _));