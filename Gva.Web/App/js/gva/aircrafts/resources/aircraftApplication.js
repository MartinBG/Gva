/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftApplication', ['$resource', function ($resource) {
    return $resource('/api/aircrafts/:id/applications/:appId');
  }]);
}(angular));
