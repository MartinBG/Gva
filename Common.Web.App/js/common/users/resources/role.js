/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('Roles', ['$resource', function ($resource) {
    return $resource('/api/roles');
  }]);
}(angular));
