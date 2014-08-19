/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportCertOperationals', ['$resource', function ($resource) {
    return $resource('api/airports/:id/airportCertOperationals/:ind', {}, {
      newCertOperational: {
        method: 'GET',
        url: 'api/airports/:id/airportCertOperationals/new'
      }
    });
  }]);
}(angular));