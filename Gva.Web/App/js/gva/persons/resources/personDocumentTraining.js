/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentTraining', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentTrainings/:ind');
  }]);
}(angular));