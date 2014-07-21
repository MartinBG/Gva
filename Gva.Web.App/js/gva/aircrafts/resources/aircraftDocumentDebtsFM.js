/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentDebtsFM', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftDocumentDebtsFM/:ind');
  }]);
}(angular));