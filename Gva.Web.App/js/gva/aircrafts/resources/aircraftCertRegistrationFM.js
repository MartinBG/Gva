/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistrationFM', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind');
  }]);
}(angular));