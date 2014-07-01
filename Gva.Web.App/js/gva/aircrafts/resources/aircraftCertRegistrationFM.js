/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistrationFM', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind', {}, {
      'getView': {
        method: 'GET',
        url: '/api/aircrafts/:id/aircraftCertRegistrationsFM/:ind/view',
        interceptor: {
          response: function (response) {
            if (response.data) {
              return response.resource;
            }

            return null;
          }
        }
      }
    });
  }]);
}(angular));
