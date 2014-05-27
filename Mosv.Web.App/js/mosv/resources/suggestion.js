/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Suggestion', ['$resource', function ($resource) {
    return $resource('/api/suggestion/:id');
  }]);
}(angular));
