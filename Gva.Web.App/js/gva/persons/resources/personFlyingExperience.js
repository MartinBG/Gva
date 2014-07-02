/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonFlyingExperiences', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/personFlyingExperiences/:ind');
  }]);
}(angular));
