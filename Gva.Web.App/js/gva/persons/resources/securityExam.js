/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('SecurityExam', ['$resource', function ($resource) {
    return $resource('/api/securityExam', {},
      {
        'extractPages': {
          method: 'POST',
          url: '/api/securityExam/extractPages'
        },
        'getAnswers': {
          method: 'GET',
          url: '/api/securityExam/getAnswers'
        },
        'calculateGrade': {
          method: 'POST',
          url: '/api/securityExam/calculateGrade'
        },
        'getPreview': {
          method: 'GET',
          url: '/api/securityExam/getPreview'
        }
      });
  }]);
}(angular));
