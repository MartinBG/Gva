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
        url: 'api/persons/printableDocs'
      },
      getStampedDocuments: {
        method: 'GET',
        url: 'api/persons/stampedDocuments'
      },
      saveStampedDocuments: {
        method: 'POST',
        url: 'api/persons/stampedDocuments',
        isArray: true
      },
      isUniqueDocData: {
        method: 'GET',
        url: 'api/persons/:id/isUniqueDocData'
      },
      getChecksForReport: {
        method: 'GET',
        url: 'api/persons/:id/getChecksForReport',
        isArray: true
      }
    });
  }]);
}(angular));
