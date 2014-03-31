/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Application', ['$resource',
      function ($resource) {
        return $resource('/api/apps/:id', { id: '@id' },
          {
            'notLinkedDocs': {
              method: 'GET',
              url: '/api/apps/notLinkedDocs'
            },
            'create': {
              method: 'POST',
              url: '/api/apps/create'
            },
            'link': {
              method: 'POST',
              url: '/api/apps/link'
            },
            'createPart': {
              method: 'POST',
              url: '/api/apps/:id/parts/create',
              params: { id: '@id' }
            },
            'linkNewPart': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkNew',
              params: { id: '@id' }
            },
            'linkExistingPart': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkExisting',
              params: { id: '@id' }
            },
            'getDocFile': {
              method: 'GET',
              url: '/api/apps/docFile',
              params: { docFileId: '@docFileId' }
            },
            'getDoc': {
              method: 'GET',
              url: '/api/apps/doc',
              params: { docId: '@docId' }
            },
            'attachDocFile': {
              method: 'POST',
              url: '/api/apps/:id/attachDocFile',
              params: { docId: '@docId' }
            }
          });
      }]);
}(angular));
