/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentApplication', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentApplications/:ind');
  }]);
}(angular));
