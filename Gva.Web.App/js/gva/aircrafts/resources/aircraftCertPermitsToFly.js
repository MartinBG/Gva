/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertPermitsToFly', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertPermitsToFly/:ind');
  }]);
}(angular));