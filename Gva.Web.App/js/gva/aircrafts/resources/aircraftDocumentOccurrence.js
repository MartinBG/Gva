
/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentOccurrence', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/documentOccurrences/:ind');
  }]);
}(angular));