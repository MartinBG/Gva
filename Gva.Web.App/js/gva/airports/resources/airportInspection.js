/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportInspections', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/inspections/:ind');
  }]);
}(angular));
