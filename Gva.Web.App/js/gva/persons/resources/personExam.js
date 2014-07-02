/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonExams',
    ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personExams/:ind');
  }]);
}(angular));