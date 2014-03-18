/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentOwner', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftDocumentOwners/:ind');
  }]);
}(angular));