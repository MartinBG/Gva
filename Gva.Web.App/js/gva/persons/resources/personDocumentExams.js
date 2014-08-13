/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentExams', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentExams/:ind', {}, {
      newExam: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentExams/new'
      }
    });
  }]);
}(angular));