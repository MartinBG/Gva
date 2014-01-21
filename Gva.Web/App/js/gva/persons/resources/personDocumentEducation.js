/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentEducation', ['$resource', function($resource) {
    return $resource('/api/persons/:id/personDocumentEducations/:ind');
  }]);
}(angular));