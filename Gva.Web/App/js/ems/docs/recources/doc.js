/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Doc', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id', { id: '@docId' },
          {
            //'createNew': {
            //  method: 'POST',
            //  url: '/api/docs/new/create'
            //},
            'createChild': {
              method: 'POST',
              url: '/api/docs/:id/create',
              params: {
                docEntryTypeAlias: '@docEntryTypeAlias'
              }
            },
            //?
            'units': {
              method: 'GET',
              url: '/api/nomenclatures/units',
              isArray: true
            },
            'register': {
              method: 'POST',
              url: '/api/docs/:id/register',
              params: {
                docVersion: '@docVersion'
              }
            },
            'setCasePart': {
              method: 'POST',
              url: '/api/docs/:id/setCasePart',
              params: {
                docVersion: '@docVersion',
                docCasePartTypeId: '@docCasePartTypeId'
              }
            },
            'setDocType': {
              method: 'POST',
              url: '/api/docs/:id/setDocType',
              params: {
                docVersion: '@docVersion'
              }
            }
          });
      }]);
}(angular));
