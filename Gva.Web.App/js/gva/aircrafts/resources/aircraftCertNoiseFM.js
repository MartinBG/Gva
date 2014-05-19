/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertNoiseFM', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertNoisesFM/:ind');
  }]);
}(angular));