/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftData', ['$resource', function($resource) {
    return $resource('/api/aircrafts/:id/aircraftData');
  }]);
}(angular));
