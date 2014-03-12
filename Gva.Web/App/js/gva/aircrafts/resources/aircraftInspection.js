/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftInspection', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/inspections/:ind');
  }]);
}(angular));