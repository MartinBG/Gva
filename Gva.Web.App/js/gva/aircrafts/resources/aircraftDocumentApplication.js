/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentApplication', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftDocumentApplications/:ind');
  }]);
}(angular));
