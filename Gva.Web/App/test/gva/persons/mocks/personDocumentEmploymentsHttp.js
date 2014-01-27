/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/personDocumentEmployments?hdate&orgid',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.hdate || $params.orgid) {
            var employments = [],
                exists;
            angular.forEach(person.personDocumentEmployments, function (employment) {
              if ($params.hdate) {
                var newDate = $filter('date')($params.hdate, 'mediumDate'),
                    oldDate = $filter('date')(employment.part.hiredate, 'mediumDate');
                exists = newDate === oldDate;
              } else if ($params.orgid) {
                var typeId = parseInt($params.orgid, 10);
                exists = employment.part.organization.nomTypeValueId === typeId;
              }
              if (exists) {
                employments.push(employment);
              }
            });
            return [200, employments];
          } else {
            return [200, person.personDocumentEmployments];
          }
        })
      .when('GET', '/api/persons/:id/personDocumentEmployments/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentEmployment = _(person.personDocumentEmployments)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentEmployment) {
            return [200, personDocumentEmployment];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personDocumentEmployments',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentEmployment = $jsonData;

          personDocumentEmployment.partIndex = person.nextIndex++;

          person.personDocumentEmployments.push(personDocumentEmployment);

          return [200];
        })
      .when('POST', '/api/persons/:id/personDocumentEmployments/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentEmployment = _(person.personDocumentEmployments)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentEmployment, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/personDocumentEmployments/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentEmploymentInd = _(person.personDocumentEmployments)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personDocumentEmployments.splice(personDocumentEmploymentInd, 1);

          return [200];
        });
  });
}(angular, _));