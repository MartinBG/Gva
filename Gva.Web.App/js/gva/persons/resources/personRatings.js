/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRatings', ['$resource', function($resource) {
    return $resource('api/persons/:id/ratings/:ind', {}, {
      newRating: {
        method: 'GET',
        url: 'api/persons/:id/ratings/new'
      },
      newRatingEdition: {
        method: 'GET',
        url: 'api/persons/:id/ratings/:ind/newEdition'
      }
    });
  }]);
}(angular));
