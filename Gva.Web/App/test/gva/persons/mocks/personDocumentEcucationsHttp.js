/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentEducations?number&typeid&publ&datef',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personDocumentEducations];

        })
      .when('GET', '/api/persons/:id/personDocumentEducations/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var personDocumentEducation = _(person.personDocumentEducations)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentEducation) {
            return [200, personDocumentEducation];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentEducations',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentEducation = $jsonData;

            personDocumentEducation.partIndex = person.nextIndex++;

            person.personDocumentEducations.push(personDocumentEducation);

            return [200, person];
          })
        .when('POST', '/api/persons/:id/personDocumentEducations/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentEducation = _(person.personDocumentEducations)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();

            _.assign(personDocumentEducation, $jsonData);

            return [200];
          })
        .when('DELETE', '/api/persons/:id/personDocumentEducations/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentEducationInd = _(person.personDocumentEducation)
              .findIndex({ partIndex: parseInt($params.ind, 10) });

            person.personDocumentEducations.splice(personDocumentEducationInd, 1);

            return [200];
          });
  });
}(angular, _));