/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistrationsFM', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertRegistrationsFM/:ind', {}, {
      'newCertRegistrationFM': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/new'
      },
      'getView': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/:ind/view',
        interceptor: {
          response: function (response) {
            if (response.data) {
              return response.resource;
            }

            return null;
          }
        }
      },
      'getDebts': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/:ind/debts',
        isArray: true
      }
    });
  }]);
}(angular));
