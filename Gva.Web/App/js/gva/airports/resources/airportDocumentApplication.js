/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportDocumentApplication', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/airportDocumentApplications/:ind');
  }]);
}(angular));
