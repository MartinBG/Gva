/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsReports', ['$resource', function ($resource) {
    return $resource('api/reports/persons', {}, {
      getDocuments: {
        method: 'GET',
        url: 'api/reports/persons/documents',
        isArray: true
      },
      getLicences: {
        method: 'GET',
        url: 'api/reports/persons/licenceCerts',
        isArray: true
      }
    });
  }]);
}(angular));
