/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportDocumentOther', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/airportDocumentOthers/:ind');
  }]);
}(angular));
