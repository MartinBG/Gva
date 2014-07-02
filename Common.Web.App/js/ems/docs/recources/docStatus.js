/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocStatuses', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id/status', {
          id: '@docId',
          docVersion: '@docVersion'
        },
          {
            'next': {
              method: 'POST',
              url: '/api/docs/:id/next',
              params: {
                alias: '',
                closure: null,
                checkedIds: []
              }
            },
            'reverse': {
              method: 'POST',
              url: '/api/docs/:id/reverse',
              params: {
                alias: ''
              }
            },
            'cancel': {
              method: 'POST',
              url: '/api/docs/:id/cancel'
            }
          });
      }]);
}(angular));
