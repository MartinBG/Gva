/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertSmod', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertSmods/:ind');
  }]);
}(angular));