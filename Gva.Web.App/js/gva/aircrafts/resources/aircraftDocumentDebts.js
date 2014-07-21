/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentDebts', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftDocumentDebts/:ind');
  }]);
}(angular));