/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftParts', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftParts/:ind', {}, {
      'newPart': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftParts/new'
      }
    });
  }]);
}(angular));