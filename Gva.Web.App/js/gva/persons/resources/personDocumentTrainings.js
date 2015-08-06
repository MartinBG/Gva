/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentTrainings', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentTrainings/:ind', {}, {
      newTraining: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentTrainings/new'
      },
      getExams: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentTrainings/exams',
        isArray: true
      },
      getTrainingView: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentTrainings/:ind/view'
      }
    });
  }]);
}(angular));