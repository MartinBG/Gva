/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentEmployment', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentEmployments/:ind');
  }]);
}(angular));
