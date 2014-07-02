/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftsInventory', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/inventory');
  }]);
}(angular));