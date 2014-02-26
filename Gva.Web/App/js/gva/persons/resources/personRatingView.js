/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRatingView', ['$resource', function($resource) {
    return $resource('/api/persons/:id/ratingViews');
  }]);
}(angular));
