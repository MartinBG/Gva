/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    function personMapper(p) {
      if (!!p) {
        return {
          id: p.lotId,
          lin: p.personData.part.lin,
          uin: p.personData.part.uin,
          names: p.personData.part.firstName + ' ' +
            p.personData.part.middleName + ' ' + p.personData.part.lastName,
          /*jshint -W052*/
          age: ~~((Date.now() - new Date(p.personData.part.dateOfBirth)) / (31557600000))
          /*jshint +W052*/
        };
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/applications/:id',
        function ($params, $filter, applicationsFactory) {
          var application = _(applicationsFactory.getInstance())
            .filter({ applicationId: parseInt($params.id, 10) }).first();

          if (application) {
            if (application.person.lotId) { //todo change person data better
              application.person = personMapper(application.person);
            }

            return [200, application];
          }
          else {
            return [404];
          }

        })

      .when('GET', '/api/applications/new/create',
        function () {
          var newApplication = {
            applicationId: undefined,
            docId: undefined,
            personLotId: undefined
          };

          return [200, newApplication];

        })
      .when('POST', '/api/applications/new/save',
        function ($jsonData, applications) {
          if (!$jsonData || !$jsonData.docId || !$jsonData.personLotId) {
            return [400];
          }

          var nextApplicationId = (!nextApplicationId) ?
            1 : _(applications).pluck('applicationId').max().value() + 1;

          $jsonData.applicationId = nextApplicationId;
          applications.push($jsonData);

          return [200, $jsonData];
        })
      .when('GET', '/api/nomenclatures/persons',
        function (personLots) {
          var noms= [],
            nomItem = {
              nomTypeValueId: '',
              name: '',
              content: []
            };

          _.forEach(personLots, function (item) {
            var t = {};

            nomItem.nomTypeValueId = item.lotId;
            nomItem.name = item.personData.part.firstName + ' ' + item.personData.part.lastName;
            nomItem.content = item;

            _.assign(t, nomItem, true);
            noms.push(t);
          });

          return [200, noms];
        });
  });
}(angular, _));
