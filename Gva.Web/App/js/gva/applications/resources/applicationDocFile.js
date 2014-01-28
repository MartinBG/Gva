/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('ApplicationDocFile', ['$resource',
      function ($resource) {
        return $resource('/api/applications/:id/docFiles', { id: '@id' },
          {
            'linkNew': {
              method: 'POST',
              url: '/api/applications/:id/docFiles/:docFileId/linkNew',
              params: { id: '@id', docFileId: '@docFileId' }
            },
            'linkExisting': {
              method: 'POST',
              url: '/api/applications/:id/docFiles/:docFileId/linkExisting',
              params: { id: '@id', docFileId: '@docFileId' }
            }
          });
      }]);
}(angular));
