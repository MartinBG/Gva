/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Application', ['$resource',
      function ($resource) {
        return $resource('/api/applications/:id', { id: '@id' },
          {
            'createNew': {
              method: 'GET',
              url: '/api/applications/new/create'
            },
            'saveNew': {
              method: 'POST',
              url: '/api/applications/new/save'
            }
          });
      }]);
}(angular));
