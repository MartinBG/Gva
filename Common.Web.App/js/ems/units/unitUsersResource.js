/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common')

  .factory('UnitUsersResource', ['$resource', function ($resource) {
    return $resource('api/units/:id/users/:userId', { id: '@id', userId: '@userId' }, {
    });
  }]);

}(angular));
