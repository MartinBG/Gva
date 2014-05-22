/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonExamAnswers', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/examAnswers');
  }]);
}(angular));