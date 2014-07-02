/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentIds', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personDocumentIds/:ind');
  }]);
}(angular));
