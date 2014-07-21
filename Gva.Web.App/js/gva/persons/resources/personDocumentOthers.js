/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonDocumentOthers', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personDocumentOthers/:ind');
  }]);
}(angular));
