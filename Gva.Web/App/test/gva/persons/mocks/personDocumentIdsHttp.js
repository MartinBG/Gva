/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentIds?number&typeid&publ&datef',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if($params.number || $params.typeid || $params.publ || $params.datef){
            var documentIds = [],
                exists;
            angular.forEach(person.personDocumentIds, function(documentId){
                if($params.number) {
                  exists = documentId.part.documentNumber === $params.number;
                } else if($params.typeid) {
                  var typeId = parseInt($params.typeid, 10);
                  exists = documentId.part.personDocumentIdType.nomTypeValueId === typeId;
                } else if ($params.publ) {
                  exists = documentId.part.documentPublisher === $params.publ;
                } else if ($params.datef) {
                  var newDate = $filter('date')($params.datef,'mediumDate'),
                    oldDate =  $filter('date')(documentId.part.documentDateValidFrom,'mediumDate');
                  exists = newDate === oldDate;
                }
                if(exists){
                  documentIds.push(documentId);
                }
              });
            return [200, documentIds];
          } else {
            return [200, person.personDocumentIds];
          }
        })
      .when('GET', '/api/persons/:id/personDocumentIds/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();
          var personDocumentId = _(person.personDocumentIds)
          .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentId) {
            return [200, personDocumentId];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentIds',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentId = $jsonData;

            personDocumentId.partIndex = person.nextIndex++;

            person.personDocumentIds.push(personDocumentId);

            return [200, person];
          })
        .when('POST', '/api/persons/:id/personDocumentIds/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentId = _(person.personDocumentIds)
              .filter({ partIndex: parseInt($params.ind, 10) }).first();

            _.assign(personDocumentId, $jsonData);

            return [200];
          })
        .when('DELETE', '/api/persons/:id/personDocumentIds/:ind',
          function ($params, $jsonData, personLots) {
            var person = _(personLots)
              .filter({ lotId: parseInt($params.id, 10) }).first();

            var personDocumentIdInd = _(person.personDocumentId)
              .findIndex({ partIndex: parseInt($params.ind, 10) });

            person.personDocumentIds.splice(personDocumentIdInd, 1);

            return [200];
          });
  });
}(angular, _));
