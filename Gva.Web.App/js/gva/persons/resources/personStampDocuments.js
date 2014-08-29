/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonStampedDocuments', ['$resource', function ($resource) {
    return $resource('api/persons/stampedDocuments');
  }]);
}(angular));
