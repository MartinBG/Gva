/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentEmployments', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentEmployments/:ind', {}, {
      newEmployment: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentEmployments/new'
      }
    });
  }]);
}(angular));
