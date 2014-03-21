/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftApex', ['$resource', function($resource) {
    return $resource('/api/aircraftsApex/:id');
  }]);
}(angular));
