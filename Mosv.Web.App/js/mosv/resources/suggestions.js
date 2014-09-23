/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Suggestions', ['$resource', function ($resource) {
    return $resource('api/suggestion/:id', {}, {
      newSuggestion: {
        method: 'GET',
        url: 'api/suggestion/new'
      },
      'fastSave': {
        method: 'POST',
        url: 'api/suggestion/:id/fastSave'
      },
      'getDocs': {
        method: 'GET',
        url: 'api/admissions/docs'
      },
      'loadData': {
        method: 'POST',
        url: 'api/suggestion/:id/loadData'
      }
    });
  }]);
}(angular));
