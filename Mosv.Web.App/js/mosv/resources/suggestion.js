/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Suggestions', ['$resource', function ($resource) {
    return $resource('/api/suggestion/:id');
  }]);
}(angular));
