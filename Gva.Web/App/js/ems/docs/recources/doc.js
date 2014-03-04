/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Doc', ['$resource',
      function ($resource) {
        return $resource('/api/docs/:id', { id: '@docId' },
          {
            'createNew': {
              method: 'POST',
              url: '/api/docs/new/create'
            },
            'registerNew': {
              method: 'POST',
              url: '/api/docs/new/register'
            },
            //?
            'units': {
              method: 'GET',
              url: '/api/nomenclatures/units',
              isArray: true
            },
            'nextStatus': {
              method: 'POST',
              url: '/api/docs/:id/nextStatus'
            },
            'reverseStatus': {
              method: 'POST',
              url: '/api/docs/:id/reverseStatus'
            },
            'cancelStatus': {
              method: 'POST',
              url: '/api/docs/:id/cancelStatus'
            },
            'setRegUri': {
              method: 'POST',
              url: '/api/docs/:id/setRegUri'
            },
            'setCasePart': {
              method: 'POST',
              url: '/api/docs/:id/setCasePart'
            },
            'setDocType': {
              method: 'POST',
              url: '/api/docs/:id/setDocType'
            }
          });
      }]);
}(angular));
