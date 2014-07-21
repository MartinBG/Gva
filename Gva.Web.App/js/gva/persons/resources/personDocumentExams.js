/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentExams',
    ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentExams/:ind');
  }]);
}(angular));