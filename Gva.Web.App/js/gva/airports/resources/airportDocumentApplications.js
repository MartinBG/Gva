/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportDocumentApplications', ['$resource', function ($resource) {
    return $resource('api/airports/:id/airportDocumentApplications/:ind', {}, {
      newApplication: {
        method: 'GET',
        url: 'api/airports/:id/airportDocumentApplications/new'
      }
    });
  }]);
}(angular));
