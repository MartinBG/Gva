/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonFlyingExperiences', ['$resource', function ($resource) {
    return $resource('api/persons/:id/personFlyingExperiences/:ind', {}, {
      newFlyingExperience: {
        method: 'GET',
        url: 'api/persons/:id/personFlyingExperiences/new'
      }
    });
  }]);
}(angular));
