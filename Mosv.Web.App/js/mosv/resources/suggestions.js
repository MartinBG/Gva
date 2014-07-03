﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('mosv').factory('Suggestions', ['$resource', function ($resource) {
    return $resource('/api/suggestion/:id', {}, {
      'fastSave': {
        method: 'POST',
        url: '/api/suggestion/:id/fastSave'
      },
      'getDocs': {
        method: 'GET',
        url: '/api/docs/forSelect'
      },
      'loadData': {
        method: 'POST',
        url: '/api/suggestion/:id/loadData'
      }
    });
  }]);
}(angular));
