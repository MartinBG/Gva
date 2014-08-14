/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistrations', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertRegistrations/:ind', {}, {
      'newCertRegistration': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrations/new'
      }
    });
  }]);
}(angular));