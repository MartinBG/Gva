/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftDocumentOther', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftDocumentOthers/:ind');
  }]);
}(angular));