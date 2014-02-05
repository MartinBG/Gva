/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Application', ['$resource',
      function ($resource) {
        return $resource('/api/applications/:id', { id: '@id' },
          {
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
