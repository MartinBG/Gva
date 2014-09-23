/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRatingEditions', ['$resource', function ($resource) {
    return $resource('api/persons/:id/ratings/:ind/ratingEditions/:index', {}, {
      newRatingEdition: {
        method: 'GET',
        url: 'api/persons/:id/ratings/:ind/ratingEditions/new'
      }
    });
  }]);
}(angular));
