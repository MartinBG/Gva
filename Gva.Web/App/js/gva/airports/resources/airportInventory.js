/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportInventory', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/inventory');
  }]);
}(angular));