/*global angular, getData*/
(function (angular, getData) {
  'use strict';
  var nomenclatures = getData('nomenclatures.sample');

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/:alias',
        function ($params) {
          return [
            200,
            nomenclatures[$params.alias]
          ];
        }
      );
  });
}(angular, getData));
