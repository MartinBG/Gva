/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftInventories', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/inventory');
  }]);
}(angular));