/*global angular, require*/
(function (angular, require) {
  'use strict';
  var nomenclatures = require('./nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/:alias?term&parentId&type&id',
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

            if ($params.type) {
              res = $filter('filter')(
                res,
                function (nom) {
                  return nom.content.type === $params.type;
                },
                true);
            }
          }

          return [200, res];
        }
      );
  });
}(angular, require));
