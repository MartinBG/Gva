/*global angular*/
(function (angular) {
  'use strict';

  angular.module('users').factory('users.Role', ['$resource', function ($resource) {
    return $resource('/api/roles');
  }]);
}(angular));
