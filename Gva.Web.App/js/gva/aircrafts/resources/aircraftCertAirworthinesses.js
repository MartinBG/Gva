/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertAirworthinesses', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertAirworthinesses/:ind');
  }]);
}(angular));