/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportsData', ['$resource', function ($resource) {
    return $resource('api/airports/:id/airportData');
  }]);
}(angular));
