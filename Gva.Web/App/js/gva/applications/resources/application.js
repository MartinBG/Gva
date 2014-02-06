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
            },
            'partsNew': {
              method: 'POST',
              url: '/api/apps/:id/parts/new',
              params: { id: '@id' }
            },
            'partsLinkNew': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkNew',
              params: { id: '@id' }
            },
            'partslinkExisting': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkExisting',
              params: { id: '@id' }
            }
          });
      }]);
}(angular));
