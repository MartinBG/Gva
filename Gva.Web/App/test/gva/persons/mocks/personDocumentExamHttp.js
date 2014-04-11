/*global angular, _*/
/*jslint maxlen: 200 */
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentExams?number&otypeid&publ&datef',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personDocumentExams];
        })
      .when('GET', '/api/persons/:id/personDocumentExams/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var personDocumentExam = _(person.personDocumentExams)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentExam) {
            return [200, personDocumentExam];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentExams',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentExam = $jsonData;

            personDocumentExam.partIndex = person.nextIndex++;

            person.personDocumentExams.push(personDocumentExam);

            return [200, { partIndex: personDocumentExam.partIndex }];
          })
        .when('POST', '/api/persons/:id/personDocumentExams/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentExam = _(person.personDocumentExams)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();

            _.assign(personDocumentExam, $jsonData);

            return [200];
          })
        .when('DELETE', '/api/persons/:id/personDocumentExams/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentExamInd = _(person.personDocumentExams)
              .findIndex({ partIndex: parseInt($params.ind, 10) });

            person.personDocumentExams.splice(personDocumentExamInd, 1);

            return [200];
          });
  });
}(angular, _));