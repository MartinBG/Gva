/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentApplications', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftDocumentApplications/:ind');
  }]);
}(angular));
