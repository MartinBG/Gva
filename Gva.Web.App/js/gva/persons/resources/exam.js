/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Exam', ['$resource', function ($resource) {
    return $resource('/api/exam', {},
      {
        'getAnswers': {
          method: 'GET',
          url: '/api/exam/getAnswers'
        },
        'calculateGrade': {
          method: 'POST',
          url: '/api/exam/calculateGrade'
        }
      });
  }]);
}(angular));
