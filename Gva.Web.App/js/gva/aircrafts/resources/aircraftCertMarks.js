/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertMarks', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertMarks/:ind');
  }]);
}(angular));