/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertPermitToFly', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertPermitsToFly/:ind');
  }]);
}(angular));