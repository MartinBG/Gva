/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentMedicals?num&nums&nump&medclid',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.num || $params.nums || $params.nump || $params.medclid) {
            var personDocumentMedicals = [],
                exists;
            angular.forEach(person.personDocumentMedicals, function (medical) {
              if ($params.num) {
                exists = medical.part.documentNumber === $params.num;
              } else if ($params.nums) {
                exists = medical.part.documentNumberSuffix === $params.nums;
              } else if ($params.nump) {
                exists = medical.part.documentNumberPrefix === $params.nump;
              } else if ($params.medclid) {
                var typeId = parseInt($params.medclid, 10);
                exists = medical.part.medClassType.nomTypeValueId === typeId;
              }
              if (exists) {
                personDocumentMedicals.push(medical);
              }
            });
            return [200, personDocumentMedicals];
          } else {
            return [200, person.personDocumentMedicals];
          }
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

          return [200];
        })
      .when('POST', '/api/persons/:id/personDocumentMedicals/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentMedical = _(person.personDocumentMedicals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentMedical, $jsonData);

          return [200];
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