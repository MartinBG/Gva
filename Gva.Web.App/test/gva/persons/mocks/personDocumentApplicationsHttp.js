/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/persons/:id/personDocumentApplications?number',
        function ($params, $filter, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          if ($params.number) {
            var applications = [];
            angular.forEach(person.personDocumentApplications, function (application) {
              if ($params.number) {
                if (application.part.documentNumber === $params.number) {
                  applications.push(application);
                }
              }
            });
            return [200, applications];
          } else {
            return [200, person.personDocumentApplications];
          }
        })
      .when('GET', 'api/persons/:id/personDocumentApplications/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentApplication = _(person.personDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personDocumentApplication) {
            return [200, personDocumentApplication];
          }
          else {
            return [404];
          }
        })
      .when('POST', 'api/persons/:id/personDocumentApplications',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentApplication = $jsonData;

          personDocumentApplication.partIndex = person.nextIndex++;

          person.personDocumentApplications.push(personDocumentApplication);

          return [200];
        })
      .when('POST', 'api/persons/:id/personDocumentApplications/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentApplication = _(person.personDocumentApplications)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personDocumentApplication, $jsonData);

          return [200];
        })
      .when('DELETE', 'api/persons/:id/personDocumentApplications/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personDocumentApplicationInd = _(person.personDocumentApplications)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personDocumentApplications.splice(personDocumentApplicationInd, 1);

          return [200];
        });
  });
}(angular, _));