/*global angular, getData*/
(function (angular, getData) {
  'use strict';
  var nomenclatures = getData('nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/:alias?term&parentId',
        function ($params, $filter) {
          var noms;

          if (!$params.parentId) {
            noms = $filter('filter')(nomenclatures[$params.alias], { name: $params.term });
          } else {
            noms = $filter('filter')(
              $filter('filter')(nomenclatures[$params.alias], { name: $params.term }),
              { parentId: parseInt($params.parentId, 10) },
              true);
          }

          return [200, noms];
        }
      );
  });
}(angular, getData));
