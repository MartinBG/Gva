/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRatings', ['$resource', function($resource) {
    return $resource('api/persons/:id/ratings/:ind', {}, {
      newRating: {
        method: 'GET',
        url: 'api/persons/:id/ratings/new'
      },
      lastEditionIndex: {
        method: 'GET',
        url: 'api/persons/:id/ratings/:ind/lastEditionIndex'
      },
      isValid: {
        method: 'POST',
        url: 'api/persons/:id/ratings/isValid'
      },
      getRatingsByValidity: {
        method: 'GET',
        url: 'api/persons/:id/ratings/byValidity',
        isArray: true
      }
    });
  }]);
}(angular));
