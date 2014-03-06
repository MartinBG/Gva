/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentOthers?number&typeid&publ&datef&pnumber',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personDocumentOthers];
        })
      .when('GET', '/api/persons/:id/personDocumentOthers/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentOther = _(person.personDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentOther) {
            return [200, personDocumentOther];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentOthers',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentOther = $jsonData;

          personDocumentOther.partIndex = person.nextIndex++;

          person.personDocumentOthers.push(personDocumentOther);

          return [200];
        })
      .when('POST', '/api/persons/:id/personDocumentOthers/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentOther = _(person.personDocumentOthers)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentOther, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/personDocumentOthers/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentOtherInd = _(person.personDocumentOthers)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personDocumentOthers.splice(personDocumentOtherInd, 1);

          return [200];
        });
  });
}(angular, _));