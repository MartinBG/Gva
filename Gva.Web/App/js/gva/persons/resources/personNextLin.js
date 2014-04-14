/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonNextLin', ['$resource', function($resource) {
    return $resource('/api/persons/:id/nextLin');
  }]);
}(angular));
