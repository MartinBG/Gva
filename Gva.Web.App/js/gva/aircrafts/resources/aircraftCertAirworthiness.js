/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertAirworthiness', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertAirworthinesses/:ind');
  }]);
}(angular));