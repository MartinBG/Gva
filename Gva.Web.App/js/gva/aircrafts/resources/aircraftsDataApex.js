/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftsDataApex', ['$resource', function($resource) {
    return $resource('api/aircrafts/:id/aircraftDataApex');
  }]);
}(angular));
