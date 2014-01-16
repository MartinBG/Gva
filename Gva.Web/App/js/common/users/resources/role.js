/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('Role', ['$resource', function ($resource) {
    return $resource('/api/roles');
  }]);
}(angular));
