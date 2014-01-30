/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('ApplicationDocFile', ['$resource',
      function ($resource) {
        return $resource('/api/apps/:id/docParts', { id: '@id' },
          {
            'linkNew': {
              method: 'POST',
              url: '/api/apps/:id/docParts/:setPartId/linkNew',
              params: { id: '@id', setPartId: '@setPartId' }
            },
            'linkExisting': {
              method: 'POST',
              url: '/api/apps/:id/docParts/:setPartId/linkExisting',
              params: { id: '@id', setPartId: '@setPartId' }
            }
          });
      }]);
}(angular));
