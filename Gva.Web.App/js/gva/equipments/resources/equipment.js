/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Equipments', ['$resource', function ($resource) {
    return $resource('/api/equipments/:id');
  }]);
}(angular));
