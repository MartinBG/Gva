/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertNoises', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertNoises/:ind', {}, {
      'newCertNoise': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertNoises/new'
      }
    });
  }]);
}(angular));