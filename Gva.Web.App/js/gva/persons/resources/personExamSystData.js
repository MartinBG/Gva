/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsExamSystData', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personExamSystData', {}, {
      newState: {
        method: 'GET',
        url: 'api/persons/:id/personExamSystData/newState'
      },
      saveState: {
        method: 'POST',
        url: 'api/persons/:id/personExamSystData/saveState'
      }
    });
  }]);
}(angular));
