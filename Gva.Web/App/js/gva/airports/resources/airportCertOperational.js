/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportCertOperational', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/airportCertOperationals/:ind');
  }]);
}(angular));