/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentMedicals?num&nums&nump&medclid',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personDocumentMedicals];

        })
      .when('GET', '/api/persons/:id/personDocumentMedicals/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentMedical = _(person.personDocumentMedicals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentMedical) {
            return [200, personDocumentMedical];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentMedicals',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentMedical = $jsonData;

          personDocumentMedical.partIndex = person.nextIndex++;

          person.personDocumentMedicals.push(personDocumentMedical);

          return [200, {partIndex: personDocumentMedical.partIndex}];
        })
      .when('POST', '/api/persons/:id/personDocumentMedicals/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentMedical = _(person.personDocumentMedicals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentMedical, $jsonData);

          return [200, { partIndex: personDocumentMedical.partIndex }];
        })
      .when('DELETE', '/api/persons/:id/personDocumentMedicals/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentMedicalInd = _(person.personMDocumentedicals)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personDocumentMedicals.splice(personDocumentMedicalInd, 1);

          return [200];
        });
  });
}(angular, _));