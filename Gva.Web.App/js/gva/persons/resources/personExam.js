/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonExam',
    ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personExams/:ind');
  }]);
}(angular));