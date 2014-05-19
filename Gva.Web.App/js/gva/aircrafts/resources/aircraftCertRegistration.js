/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistration', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertRegistrations/:ind');
  }]);
}(angular));