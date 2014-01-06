/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Corr', ['$resource',
      function ($resource) {
        return $resource('/api/corrs/:corrId', {userId:'@corrId'},
          {
            'create': {
              method: 'GET',
              url: '/api/corrs/new'
            },
            'contact': {
              method: 'GET',
              url: '/api/corrs/contacts/new/:corrId',
              params: { corrId: '@corrId' }
            }
          });
      }]);
}(angular));
