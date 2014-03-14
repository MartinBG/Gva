/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentDebt', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftDocumentDebts/:ind');
  }]);
}(angular));