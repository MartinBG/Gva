/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva')
    .factory('Application', ['$resource',
      function ($resource) {
        return $resource('/api/applications/:id', { id: '@id' },
          {
            'validateExist': {
              method: 'POST',
              url: '/api/applications/validate/exist'
            }
          });
      }]);
}(angular));
