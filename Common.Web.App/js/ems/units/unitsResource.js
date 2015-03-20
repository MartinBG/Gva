/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common')

  .factory('UnitsResource', ['$resource', function ($resource) {
    return $resource('api/units/:id', { id: '@unitId'}, {
      setActiveStatus: {
        method: 'POST',
        url: 'api/units/:id/status/:isActive',
        params: { id: '@unitId', isActive: '@isActive' }
      }
    });
  }]);

}(angular));
