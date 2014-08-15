/*global angular*/
(function (angular) {
  'use strict';

  angular.module('aop')
    .factory('Aops', ['$resource',
      function ($resource) {
        return $resource('api/aop/apps/:id', { id: '@appId' },
          {
            'loadChecklist': {
              method: 'GET',
              url: 'api/aop/editableFiles/checklist'
            },
            'getNew': {
              method: 'POST',
              url: 'api/aop/apps/new'
            },
            'generateChecklist': {
              method: 'POST',
              url: 'api/aop/apps/:id/checklist',
              params: {
                action: '',
                identifier: ''
              }
            },
            'generateNote': {
              method: 'POST',
              url: 'api/aop/apps/:id/note'
            },
            'generateReport': {
              method: 'POST',
              url: 'api/aop/apps/:id/report'
            },
            'readFedForFirstStage': {
              method: 'POST',
              url: 'api/aop/apps/:id/fed/first'
            },
            'readFedForSecondStage': {
              method: 'POST',
              url: 'api/aop/apps/:id/fed/second'
            },
            'findAopApp': {
              method: 'GET',
              url: 'api/aop/apps/docs/:id'
            },
            'getProperDocTypeForApp': {
              method: 'GET',
              url: 'api/aop/apps/properDocType',
              params: {
                type: ''
              }
            },
            'getDocs': {
              method: 'GET',
              url: 'api/aop/apps/docs'
            }
          });
      }]);
}(angular));
