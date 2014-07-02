/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('SuggestionsData', ['$resource', function ($resource) {
    return $resource('/api/suggestion/:id/suggestionData');
  }]);
}(angular));
