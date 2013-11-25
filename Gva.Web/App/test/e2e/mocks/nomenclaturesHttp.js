/*global angular, getData*/
(function (angular, getData) {
  'use strict';
  var nomenclatures = getData('nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/:alias?term',
        function ($params, $filter) {
          var noms = $filter('filter')(nomenclatures[$params.alias], { name: $params.term });
          return [200, noms];
        }
      );
  });
}(angular, getData));
