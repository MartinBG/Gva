/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLastLicenceNumber', ['$resource', function($resource) {
    return $resource('/api/persons/:id/lastLicenceNumber');
  }]);
}(angular));
