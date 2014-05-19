/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentOther', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personDocumentOthers/:ind');
  }]);
}(angular));
