/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertAirworthinessFM', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertAirworthinessesFM/:ind');
  }]);
}(angular));