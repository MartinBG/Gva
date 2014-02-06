/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('Corr', ['$resource',
      function ($resource) {
        return $resource('/api/corrs/:corrId', { corrId: '@corrId' });
      }]);
}(angular));
