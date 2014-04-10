/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AirportDocumentOwner', ['$resource', function ($resource) {
    return $resource('/api/airports/:id/airportDocumentOwners/:ind');
  }]);
}(angular));
