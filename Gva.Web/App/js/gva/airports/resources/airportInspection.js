/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportInspection', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/inspections/:ind');
  }]);
}(angular));
