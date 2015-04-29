/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonReferences', ['$resource', function ($resource) {
    return $resource('api/persons/personReferences', {}, {
      getDocuments: {
        method: 'GET',
        url: 'api/persons/personReferences/documents',
        isArray: true
      }
    });
  }]);
}(angular));
