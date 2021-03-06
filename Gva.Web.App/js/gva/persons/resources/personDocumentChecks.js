﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentChecks', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentChecks/:ind', {}, {
      newCheck: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentChecks/new'
      },
      getReport: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentChecks/:ind/report'
      },
      getCheckView: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentChecks/:ind/view'
      }
    });
  }]);
}(angular));
