/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('AircraftCertAirworthinessesFM', ['$resource', function ($resource) {
      return $resource('api/aircrafts/:id/aircraftCertAirworthinessesFM/:ind', {}, {
        'newCertAirworthiness': {
          method: 'GET',
          url: 'api/aircrafts/:id/aircraftCertAirworthinessesFM/new'
        }
      });
    }]);
}(angular));