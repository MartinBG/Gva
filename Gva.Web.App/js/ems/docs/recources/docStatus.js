/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStatus', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/status', {
          id: '@docId',
          docVersion: '@docVersion'
        },
          {
            'next': {
              method: 'POST',
              url: '/api/docs/:id/next'
            },
            'reverse': {
              method: 'POST',
              url: '/api/docs/:id/reverse'
            },
            'cancel': {
              method: 'POST',
              url: '/api/docs/:id/cancel'
            }
          });
      }]);
}(angular));
