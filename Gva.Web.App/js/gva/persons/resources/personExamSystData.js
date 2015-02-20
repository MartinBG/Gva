/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonExamSystData', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personExamSystData', {}, {
      newState: {
        method: 'GET',
        url: 'api/persons/:id/personExamSystData/newState'
      },
      updateInfo: {
        method: 'POST',
        url: 'api/persons/:id/personExamSystData/updateInfo'
      }
    });
  }]);
}(angular));
