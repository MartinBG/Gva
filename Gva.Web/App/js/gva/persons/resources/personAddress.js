/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonAddress', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personAddresses/:ind');
  }]);
}(angular));