/*global angular*/
(function (angular) {
  'use strict';

  angular.module('aop')
    .factory('Aop', ['$resource',
      function ($resource) {
        return $resource('/api/aop/apps/:id', { id: '@appId' },
          {
            'getNew': {
              method: 'POST',
              url: '/api/aop/apps/new'
            },
            'generateChecklist': {
              method: 'POST',
              url: '/api/aop/apps/:id/checklist',
              params: {
                action: '',
                identifier: ''
              }
            },
            'generateNote': {
              method: 'POST',
              url: '/api/aop/apps/:id/note'
            },
            'generateReport': {
              method: 'POST',
              url: '/api/aop/apps/:id/report'
            }
          });
      }]);
}(angular));
