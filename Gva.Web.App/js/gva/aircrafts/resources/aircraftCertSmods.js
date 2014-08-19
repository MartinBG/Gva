/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertSmods', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertSmods/:ind', {}, {
      'newCertSmod': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertSmods/new'
      }
    });
  }]);
}(angular));