/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Doc', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id', { id: '@docId' },
          {
            'createChild': {
              method: 'POST',
              url: '/api/docs/:id/create',
              params: {
                docEntryTypeAlias: '@docEntryTypeAlias'
              }
            },
            'createChildAcknowledge': {
              method: 'POST',
              url: '/api/docs/:id/createAcknowledge'
            },
            'createChildNotAcknowledge': {
              method: 'POST',
              url: '/api/docs/:id/createNotAcknowledge'
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
            },
            'markAsRead': {
              method: 'POST',
              url: '/api/docs/:id/markAsRead',
              params: {
                docVersion: '@docVersion'
              }
            },
            'markAsUnread': {
              method: 'POST',
              url: '/api/docs/:id/markAsUnread',
              params: {
                docVersion: '@docVersion'
              }
            },
            'createTicket': {
              method: 'POST',
              url: '/api/docs/:id/createTicket',
              params: {
                docFileId: '@docFileId',
                fileKey: '@fileKey'
              }
            },
            'manualRegister': {
              method: 'POST',
              url: '/api/docs/:id/manualRegister',
              params: {
                docVersion: '@docVersion',
                regUri: '',
                regDate: ''
              }
            },
            'getRegisterIndex': {
              method: 'GET',
              url: '/api/docs/:id/registerIndex'
            }
          });
      }]);
}(angular));
