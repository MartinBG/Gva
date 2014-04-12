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
              params: {
                id: '@id',
                docId: '@docId',
                setPartAlias: '@setPartAlias'
              }
            },
            'linkNewPart': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkNew',
              params: {
                id: '@id',
                setPartAlias: '@setPartAlias'
              }
            },
            'linkExistingPart': {
              method: 'POST',
              url: '/api/apps/:id/parts/linkExisting',
              params: {
                id: '@id',
                docFileId: '@docFileId',
                partId: '@partId'
              }
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
              url: '/api/apps/:id/docFiles/create',
              params: { docId: '@docId' }
            },
            'getApplicationByDocId': {
              method: 'GET',
              url: '/api/apps/appByDocId',
              params: { docId: '@docId' }
            }
          });
      }]);
}(angular));
