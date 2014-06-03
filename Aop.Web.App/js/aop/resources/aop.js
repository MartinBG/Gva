/*global angular*/
(function (angular) {
  'use strict';

  angular.module('aop')
    .factory('Aop', ['$resource',
      function ($resource) {
        return $resource('/api/aop/apps/:id', { id: '@appId' },
          {
            'loadChecklist': {
              method: 'GET',
              url: '/api/aop/editableFiles/checklist'
            },
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
            }
          });
      }]);
}(angular));
