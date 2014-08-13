/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentEducations', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentEducations/:ind', {}, {
      newEducation: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentEducations/new'
      }
    });
  }]);
}(angular));