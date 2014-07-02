/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentMedicals', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentMedicals/:ind');
  }]);
}(angular));
