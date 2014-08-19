/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportDocumentOthers', ['$resource', function ($resource) {
    return $resource('api/airports/:id/airportDocumentOthers/:ind', {}, {
      newDocument: {
        method: 'GET',
        url: 'api/airports/:id/airportDocumentOthers/new'
      }
    });
  }]);
}(angular));
