/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRegistrationsFM', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertRegistrationsFM/:ind', {}, {
      'newCertRegistrationFM': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/new'
      },
      'removeDereg': {
        method: 'POST',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/:ind/removeDereg'
      },
      'initExportData': {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftCertRegistrationsFM/:ind/initExportData'
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
      }
    });
  }]);
}(angular));
