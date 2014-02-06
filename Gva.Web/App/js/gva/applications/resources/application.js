/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Application', ['$resource',
      function ($resource) {
        return $resource('/api/apps/:id', { id: '@id' },
          {
            'createNew': {
              method: 'POST',
              url: '/api/apps/new'
            }
          });
      }]);
}(angular));
