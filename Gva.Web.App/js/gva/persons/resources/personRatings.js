/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonRatings', ['$resource', function($resource) {
    return $resource('/api/persons/:id/ratings/:ind');
  }]);
}(angular));
