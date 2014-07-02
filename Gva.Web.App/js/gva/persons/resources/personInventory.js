/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsInventory', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/inventory');
  }]);
}(angular));