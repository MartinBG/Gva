/*global angular, _*/
/*jslint maxlen: 200 */
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentTrainings?number&otypeid&oroleid&pnumber&publ&datef',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          //if ($params.number || $params.otypeid || $params.oroleid || $params.pnumber || $params.publ || $params.datef) {
          //  var documentTrainings = [],
          //      exists;
          //  angular.forEach(person.personDocumentTrainings, function (documentTraining) {
          //    if ($params.number) {
          //      exists = documentTraining.part.documentNumber === $params.number;
          //    } else if ($params.otypeid) {
          //      var typeId = parseInt($params.otypeid, 10);
          //      exists = documentTraining.part.personOtherDocumentType.nomTypeValueId === typeId;
          //    } else if ($params.oroleid) {
          //      var roleId = parseInt($params.oroleid, 10);
          //      exists = documentTraining.part.personOtherDocumentRole.nomTypeValueId === roleId;
          //    } else if ($params.pnumber){
          //      exists = documentTraining.part.documentPersonNumber === $params.pnumber;
          //    } else if ($params.publ) {
          //      exists = documentTraining.part.documentPublisher === $params.publ;
          //    } else if ($params.datef) {
          //      var newDate = $filter('date')($params.datef, 'mediumDate'),
          //        oldDate = $filter('date')(documentTraining.part.documentDateValidFrom, 'mediumDate');
          //      exists = newDate === oldDate;
          //    }
          //    if (exists) {
          //      documentTrainings.push(documentTraining);
          //    }
          //  });
          //  return [200, documentTrainings];
          //} else {
          return [200, person.personDocumentTrainings];
        })
        //})
      .when('GET', '/api/persons/:id/personDocumentTrainings/:ind',
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
      .when('POST', '/api/persons/:id/personDocumentTrainings',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentTraining = $jsonData;

            personDocumentTraining.partIndex = person.nextIndex++;

            person.personDocumentTrainings.push(personDocumentTraining);

            return [200, person];
          })
        .when('POST', '/api/persons/:id/personDocumentTrainings/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentTraining = _(person.personDocumentTrainings)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();

            _.assign(personDocumentTraining, $jsonData);

            return [200];
          })
        .when('DELETE', '/api/persons/:id/personDocumentTrainings/:ind',
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