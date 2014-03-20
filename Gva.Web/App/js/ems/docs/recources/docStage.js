/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStage', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/stages', { id: '@docId' },
          {
            'current': {
              method: 'GET',
              url: '/api/docs/:id/stages/current'
            },
            'next': {
              method: 'POST',
              url: '/api/docs/:id/stages/next'
            },
            'edit': {
              method: 'POST',
              url: '/api/docs/:id/stages/edit'
            },
            'end': {
              method: 'POST',
              url: '/api/docs/:id/stages/end'
            },
            'reverse': {
              method: 'POST',
              url: '/api/docs/:id/stages/reverse'
            }
          });
      }]);
}(angular));
