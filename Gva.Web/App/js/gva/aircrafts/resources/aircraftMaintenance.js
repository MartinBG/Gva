/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftMaintenance', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/maintenances/:ind');
  }]);
}(angular));