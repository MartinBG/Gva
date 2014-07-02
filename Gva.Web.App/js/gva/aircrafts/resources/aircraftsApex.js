/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftsApex', ['$resource', function($resource) {
    return $resource('/api/aircraftsApex/:id');
  }]);
}(angular));
