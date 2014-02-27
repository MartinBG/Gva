/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStage', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:docId/stages', { docId: '@docId' },
          {
            'current': {
              method: 'GET',
              url: '/api/docs/:docId/stages/current'
            },
            'next': {
              method: 'POST',
              url: '/api/docs/:docId/stages/next'
            },
            'edit': {
              method: 'POST',
              url: '/api/docs/:docId/stages/edit'
            },
            'end': {
              method: 'POST',
              url: '/api/docs/:docId/stages/end'
            },
            'reverse': {
              method: 'POST',
              url: '/api/docs/:docId/stages/reverse'
            }
          });
      }]);
}(angular));
