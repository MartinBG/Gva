/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportApplication', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/applications');
  }]);
}(angular));
