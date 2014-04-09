/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Equipment', ['$resource', function($resource) {
    return $resource('/api/equipments/:id');
  }]);
}(angular));
