/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentMedicals', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentMedicals/:ind', {}, {
      newMedical: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentMedicals/new'
      },
      getMedicalView: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentMedicals/:ind/view'
      }
    });
  }]);
}(angular));
