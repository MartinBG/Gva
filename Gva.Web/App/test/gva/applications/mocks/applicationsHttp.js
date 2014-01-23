/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('GET', '/api/applications/new/create',
        function () {
          var newGvaApplication = {
            gvaApplicationId: undefined,
            docId: undefined,
            personLotId: undefined
          };

          return [200, newGvaApplication];

        })
      .when('POST', '/api/applications/new/save',
        function ($jsonData, gvaApplications) {
          if (!$jsonData || !$jsonData.docId || !$jsonData.personLotId) {
            return [400];
          }

          var nextGvaApplicationId = (!nextGvaApplicationId) ?
            1 : _(gvaApplications).pluck('gvaApplicationId').max().value() + 1;

          $jsonData.gvaApplicationId = nextGvaApplicationId;
          gvaApplications.push($jsonData);

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
