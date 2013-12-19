/*global angular*/
(function (angular) {
  'use strict';

  angular.module('corrs')
    .factory('Corr', ['$resource',
      function ($resource) {
        return $resource('/api/corrs/:corrId', {userId:'@corrId'},
          {
            'NewCorr': {
              method: 'GET',
              url: '/api/corrs/new'
            },
            'CorrContact': {
              method: 'GET',
              url: '/api/corrs/contacts/new/:corrId',
              params: { corrId: '@corrId' }
            }
          });
      }]);
}(angular));
