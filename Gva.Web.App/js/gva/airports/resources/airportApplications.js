/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportApplications', ['$resource', function ($resource) {
    return $resource('api/airports/:id/applications/:appId');
  }]);
}(angular));
