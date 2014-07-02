/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentChecks', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentChecks/:ind');
  }]);
}(angular));
