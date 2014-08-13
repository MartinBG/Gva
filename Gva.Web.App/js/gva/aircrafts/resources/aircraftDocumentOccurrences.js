
/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentOccurrences', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/documentOccurrences/:ind', {}, {
      newDocumentOccurrence: {
        method: 'GET',
        url: 'api/aircrafts/:id/documentOccurrences/new'
      }
    });
  }]);
}(angular));