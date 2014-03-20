/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocWorkflow', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/workflow', { id: '@docId' },
          {
            'add': {
              method: 'POST',
              url: '/api/docs/:id/workflow/add'
            },
            'remove': {
              method: 'POST',
              url: '/api/docs/:id/workflow/remove'
            }
          });
      }]);
}(angular));
