/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Corrs', ['$resource',
      function ($resource) {
        return $resource('/api/corrs/:id', { id: '@correspondentId' },
          {
            'getNew': {
              method: 'GET',
              url: '/api/corrs/new'
            }
          });
      }]);
}(angular));
