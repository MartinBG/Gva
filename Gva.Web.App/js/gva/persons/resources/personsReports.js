﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonsReports', ['$resource', function ($resource) {
    return $resource('api/reports/persons', {}, {
      getDocuments: {
        method: 'GET',
        url: 'api/reports/persons/documents'
      },
      getLicences: {
        method: 'GET',
        url: 'api/reports/persons/licenceCerts',
        isArray: true
      },
      getRatings: {
        method: 'GET',
        url: 'api/reports/persons/ratings',
        isArray: true
      }
    });
  }]);
}(angular));
