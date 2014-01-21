/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Unit', ['$resource',
      function ($resource) {
        return $resource('/api/units/:unitId', { unitId: '@unitId' });
      }]);
}(angular));
