/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocWorkflow', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:docId/workflow', { docId: '@docId' },
          {
            'add': {
              method: 'POST',
              url: '/api/docs/:docId/workflow/add'
            },
            'remove': {
              method: 'POST',
              url: '/api/docs/:docId/workflow/remove'
            }
          });
      }]);
}(angular));
