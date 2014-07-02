/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStages', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/stages', { id: '@docId' },
          {
            'edit': {
              method: 'POST',
              url: '/api/docs/:id/stages/edit',
              params: {
                docVersion: '@docVersion'
              }
            },
            'end': {
              method: 'POST',
              url: '/api/docs/:id/stages/end',
              params: {
                docVersion: '@docVersion'
              }
            },
            'reverse': {
              method: 'DELETE',
              url: '/api/docs/:id/stages',
              params: {
                docVersion: '@docVersion'
              }
            }
          });
      }]);
}(angular));
