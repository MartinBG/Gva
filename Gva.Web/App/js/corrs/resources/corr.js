/*global angular*/
(function (angular) {
  'use strict';

  angular.module('corrs').factory('Corr', ['$resource', function ($resource) {
    return $resource('/api/corrs/:corrId', { corrId: '@corrId' });
  }]);
}(angular));
