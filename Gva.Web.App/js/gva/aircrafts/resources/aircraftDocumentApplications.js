/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentApplications', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftDocumentApplications/:ind', {}, {
      newDocumentApplication: {
        method: 'GET',
        url: 'api/aircrafts/:id/aircraftDocumentApplications/new'
      }
    });
  }]);
}(angular));
