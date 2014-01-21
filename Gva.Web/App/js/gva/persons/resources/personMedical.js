/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonMedical', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personMedicals/:ind');
  }]);
}(angular));