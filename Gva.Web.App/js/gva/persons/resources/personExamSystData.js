/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsExamSystData', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personExamSystData');
  }]);
}(angular));
