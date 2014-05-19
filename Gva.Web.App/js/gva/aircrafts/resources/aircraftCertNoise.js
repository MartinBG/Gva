/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertNoise', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertNoises/:ind');
  }]);
}(angular));