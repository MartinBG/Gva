/*global angular, _*/
/*jslint maxlen: 200 */
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/persons/:id/personDocumentTrainings?number&otypeid&oroleid&pnumber&publ&datef',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personDocumentTrainings];
        })
      .when('GET', 'api/persons/:id/personDocumentTrainings/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var personDocumentTraining = _(person.personDocumentTrainings)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentTraining) {
            return [200, personDocumentTraining];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/persons/:id/personDocumentTrainings',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentTraining = $jsonData;

            personDocumentTraining.partIndex = person.nextIndex++;

            person.personDocumentTrainings.push(personDocumentTraining);

            return [200, { partIndex: personDocumentTraining.partIndex }];
          })
        .when('POST', 'api/persons/:id/personDocumentTrainings/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentTraining = _(person.personDocumentTrainings)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();

            _.assign(personDocumentTraining, $jsonData);

            return [200];
          })
        .when('DELETE', 'api/persons/:id/personDocumentTrainings/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentTrainingInd = _(person.personDocumentTrainings)
              .findIndex({ partIndex: parseInt($params.ind, 10) });

            person.personDocumentTrainings.splice(personDocumentTrainingInd, 1);

            return [200];
          });
  });
}(angular, _));