/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportData', ['$resource', function($resource) {
    return $resource('/api/airports/:id/airportData');
  }]);
}(angular));
