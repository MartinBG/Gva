/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('AplicationsCase', ['$resource',
      function ($resource) {
        return $resource('api/apps/:id', { id: '@id' },
          {
            newPart: {
              method: 'GET',
              url: 'api/apps/parts/:lotId/:setPartAlias/new',
              params: {
                lotId: '@lotId',
                setPartAlias: '@setPartAlias'
              }
            },
            linkNewPart: {
              method: 'POST',
              url: 'api/apps/:id/parts/linkNew',
              params: {
                id: '@id',
                setPartAlias: '@setPartAlias'
              }
            },
            linkExistingPart: {
              method: 'POST',
              url: 'api/apps/:id/parts/linkExisting',
              params: {
                id: '@id',
                docFileId: '@docFileId',
                partId: '@partId'
              }
            },
            newDocFile: {
              method: 'GET',
              url: 'api/apps/docFiles/:docId/new',
              params: { docId: '@docId' }
            },
            attachDocFile: {
              method: 'POST',
              url: 'api/apps/docFiles/:docId/create',
              params: { docId: '@docId' }
            }
          });
      }]);
}(angular));
