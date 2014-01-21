/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personMedicals?num&nums&nump&medclid',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.num || $params.nums || $params.nump || $params.medclid) {
            var personMedicals = [],
                exists;
            angular.forEach(person.personMedicals, function (medical) {
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
                personMedicals.push(medical);
              }
            });
            return [200, personMedicals];
          } else {
            return [200, person.personMedicals];
          }
        })
      .when('GET', '/api/persons/:id/personMedicals/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personMedical = _(person.personMedicals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personMedical) {
            return [200, personMedical];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personMedicals',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personMedical = $jsonData;

          personMedical.partIndex = person.nextIndex++;

          person.personMedicals.push(personMedical);

          return [200];
        })
      .when('POST', '/api/persons/:id/personMedicals/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personMedical = _(person.personMedicals)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personMedical, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/personMedicals/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personMedicalInd = _(person.personMedicals)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personMedicals.splice(personMedicalInd, 1);

          return [200];
        });
  });
}(angular, _));