/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonReferences', ['$resource', function ($resource) {
    return $resource('api/persons/references', {}, {
      getDocuments: {
        method: 'GET',
        url: 'api/persons/references/documents',
        isArray: true
      },
      getLicences: {
        method: 'GET',
        url: 'api/persons/references/licenceCerts',
        isArray: true
      }
    });
  }]);
}(angular));
