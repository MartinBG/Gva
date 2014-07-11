/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Docs', ['$resource',
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
            'createAbbcdnTicket': {
              method: 'POST',
              url: '/api/docs/:id/createAbbcdnTicket',
              params: {
                docTypeUri: '@docTypeUri',
                abbcdnKey: '@abbcdnKey'
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
            },
            'getDocsForChange': {
              method: 'GET',
              url: '/api/docs/:id/getDocsForChange'
            },
            'changeDocParent': {
              method: 'POST',
              url: '/api/docs/:id/changeDocParent',
              params: {
                newDocId: '@newDocId'
              }
            },
            'changeDocClassification': {
              method: 'POST',
              url: '/api/docs/:id/changeDocClassification'
            },
            'createNewCase': {
              method: 'POST',
              url: '/api/docs/:id/createNewCase'
            },
            'getDocSendEmail': {
              method: 'GET',
              url: '/api/docs/:id/getDocSendEmail'
            },
            'postDocSendEmail': {
              method: 'POST',
              url: '/api/docs/:id/postDocSendEmail'
            },
            'getRioEditableFile': {
              method: 'GET',
              url: '/api/docs/:id/getRioEditableFile'
            }
          });
      }]);
}(angular));
