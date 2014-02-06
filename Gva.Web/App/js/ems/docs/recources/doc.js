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
            }
          });
      }]);
}(angular));
