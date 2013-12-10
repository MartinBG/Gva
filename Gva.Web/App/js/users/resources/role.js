/*global angular*/
(function (angular) {
  'use strict';

  angular.module('users').factory('Role', ['$resource', function ($resource) {
    return $resource('/api/roles');
  }]);
}(angular));
