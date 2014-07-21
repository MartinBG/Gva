/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentOwners', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftDocumentOwners/:ind');
  }]);
}(angular));