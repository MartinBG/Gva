/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportsInventory', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/inventory');
  }]);
}(angular));