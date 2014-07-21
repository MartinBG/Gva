/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsLastLicenceNumber', ['$resource', function($resource) {
    return $resource('api/persons/:id/lastLicenceNumber');
  }]);
}(angular));
