/*global angular, _, require*/
(function (angular, _, require) {
  'use strict';
  var nomenclatures = require('./nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/persons',
        function (personLots) {
          var noms = [],
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
        })
      .when('GET', '/api/nomenclatures/units?name',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(nomenclatures.units, {
              name: $params.name
            })
          ];
        })
      .when('GET', '/api/nomenclatures/:alias?term&parentId&id',
        function ($params, $filter) {
          var res = nomenclatures[$params.alias];

          if ($params.id) {
            res = $filter('filter')(res, { nomTypeValueId: parseInt($params.id, 10) }, true)[0];
          } else {
            if ($params.term) {
              res = $filter('filter')(res, { name: $params.term }, false);
            }

            if ($params.parentId) {
              res = $filter('filter')(res, { parentId: parseInt($params.parentId, 10) }, true);
            }
          }

          return [200, res];
        }
      );
  });
}(angular, _, require));
