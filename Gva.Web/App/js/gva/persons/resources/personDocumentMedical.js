/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentMedical', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentMedicals/:ind');
  }]);
}(angular));