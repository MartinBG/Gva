/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonEmployment', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personEmployments/:ind');
  }]);
}(angular));
