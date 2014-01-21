/*global angular, require*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      units = nomenclatures.units;
    

    $httpBackendConfiguratorProvider
      .when('GET', '/api/nomenclatures/units',
        function () {
          return [
            200,
            units
          ];
        })
     .when('GET', '/api/units?name',
        function ($params, $filter) {
          return [
            200,
            $filter('filter')(units, {
              name: $params.name
            })
          ];
        });
  });
}(angular));
