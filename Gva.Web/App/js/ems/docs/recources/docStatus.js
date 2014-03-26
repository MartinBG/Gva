/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStatus', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/status', { id: '@docId' },
          {
            'next': {
              method: 'POST',
              url: '/api/docs/:id/next',
              params: {
                docVersion: '@docVersion'
              }
            },
            'reverse': {
              method: 'POST',
              url: '/api/docs/:id/reverse',
              params: {
                docVersion: '@docVersion'
              }
            },
            'cancel': {
              method: 'POST',
              url: '/api/docs/:id/cancel',
              params: {
                docVersion: '@docVersion'
              }
            }
          });
      }]);
}(angular));
