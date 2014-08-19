/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentIds', ['$resource', function($resource) {
    return $resource('api/persons/:id/personDocumentIds/:ind', {}, {
      newDocumentId: {
        method: 'GET',
        url: 'api/persons/:id/personDocumentIds/new'
      }
    });
  }]);
}(angular));
