/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentId', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personDocumentIds/:ind');
  }]);
}(angular));