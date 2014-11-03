/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('Persons', ['$resource', function ($resource) {
    return $resource('api/persons/:id', {}, {
      getNextLin: {
        method: 'GET',
        url: 'api/persons/nextLin'
      },
      isUniqueUin: {
        method: 'GET',
        url: 'api/persons/isUniqueUin'
      },
      newPerson: {
        method: 'GET',
        url: 'api/persons/new'
      },
      getPrintableDocs: {
        method: 'GET',
        url: 'api/persons/printableDocs',
        isArray: true
      },
      getStampedDocuments: {
        method: 'GET',
        url: 'api/persons/stampedDocuments',
        isArray: true
      },
      saveStampedDocuments: {
        method: 'POST',
        url: 'api/persons/stampedDocuments',
        isArray: true
      },
      isUniqueDocNumber: {
        method: 'GET',
        url: 'api/persons/:id/isUniqueDocNumber'
      }
    });
  }]);
}(angular));
