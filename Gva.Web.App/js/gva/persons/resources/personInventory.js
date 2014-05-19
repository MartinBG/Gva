/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonInventory', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/inventory');
  }]);
}(angular));