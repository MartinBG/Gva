/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Doc', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:docId', { docId: '@docId' },
          {
            'createNew': {
              method: 'POST',
              url: '/api/docs/new/create'
            },
            'registerNew': {
              method: 'POST',
              url: '/api/docs/new/register'
            },
            'units': {
              method: 'GET',
              url: '/api/nomenclatures/units',
              isArray: true
            },
            'nextStatus': {
              method: 'POST',
              url: '/api/docs/:docId/nextStatus'
            },
            'reverseStatus': {
              method: 'POST',
              url: '/api/docs/:docId/reverseStatus'
            },
            'cancelStatus': {
              method: 'POST',
              url: '/api/docs/:docId/cancelStatus'
            },
            'setRegUri': {
              method: 'POST',
              url: '/api/docs/:docId/setRegUri'
            },
            'setCasePart': {
              method: 'POST',
              url: '/api/docs/:docId/setCasePart'
            },
            'setDocType': {
              method: 'POST',
              url: '/api/docs/:docId/setDocType'
            }
          });
      }]);
}(angular));
