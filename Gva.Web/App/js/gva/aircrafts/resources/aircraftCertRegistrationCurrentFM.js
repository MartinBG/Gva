/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory(
    'AircraftCertRegistrationCurrentFM',
    ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertRegistrationsCurrent/:ind');
  }]);
}(angular));