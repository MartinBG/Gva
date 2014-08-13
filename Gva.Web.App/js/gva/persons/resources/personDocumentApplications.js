/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentApplications', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentApplications/:ind', {}, {
      newApplication: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentApplications/new'
      }
    });
  }]);
}(angular));
