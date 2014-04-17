/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCurrentDebtFM', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/aircraftCurrentDocumentDebtsFM/:ind');
  }]);
}(angular));