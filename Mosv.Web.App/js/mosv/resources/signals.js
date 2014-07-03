/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Signals', ['$resource', function ($resource) {
    return $resource('/api/signals/:id', {}, {
      'fastSave': {
        method: 'POST',
        url: '/api/signals/:id/fastSave'
      },
      'getDocs': {
        method: 'GET',
        url: '/api/docs/forSelect'
      },
      'loadData': {
        method: 'POST',
        url: '/api/signals/:id/loadData'
      }
    });
  }]);
}(angular));
