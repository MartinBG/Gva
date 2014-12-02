/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Applications', ['$resource',
      function ($resource) {
        return $resource('api/apps/:id', { id: '@id' },
          {
            notLinkedDocs: {
              method: 'GET',
              url: 'api/apps/notLinkedDocs'
            },
            create: {
              method: 'POST',
              url: 'api/apps/create'
            },
            getAppPart: {
              method: 'GET',
              url: 'api/apps/appPart/:lotId/:ind',
              params: {
                lotId: '@lotId',
                ind: '@ind'
              }
            },
            editAppPart: {
              method: 'POST',
              url: 'api/apps/appPart/:lotId/:ind',
              params: {
                lotId: '@lotId',
                ind: '@ind'
              }
            },
            link: {
              method: 'POST',
              url: 'api/apps/link'
            },
            getApplicationByDocId: {
              method: 'GET',
              url: 'api/apps/appByDocId',
              params: { docId: '@docId' }
            },
            getGvaCorrespodents: {
              method: 'GET',
              url: 'api/apps/getGvaCorrespodents',
              params: { lotId: '@lotId' }
            }
          });
      }]);
}(angular));
