/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonFlyingExperience', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personFlyingExperiences/:ind');
  }]);
}(angular));
