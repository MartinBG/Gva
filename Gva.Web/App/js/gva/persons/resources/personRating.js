/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRating', ['$resource', function($resource) {
    return $resource('/api/persons/:id/ratings');
  }]);
}(angular));
