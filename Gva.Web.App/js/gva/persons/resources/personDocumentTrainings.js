/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentTrainings', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentTrainings/:ind');
  }]);
}(angular));