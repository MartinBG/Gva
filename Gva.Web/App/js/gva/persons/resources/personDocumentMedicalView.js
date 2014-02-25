/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentMedicalView', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentMedicalViews');
  }]);
}(angular));