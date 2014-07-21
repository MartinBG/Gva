/*global angular*/
(function (angular) {
  'use strict';

  angular.module('ems')
    .factory('DocWorkflows', ['$resource',
      function ($resource) {
        return $resource('api/docs/:id/workflow/:itemId', {
          id: '@docId',
          itemId: '@itemId',
          docVersion: '@docVersion'
        });
      }]);
}(angular));
