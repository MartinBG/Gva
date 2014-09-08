/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('Users', ['$resource', function ($resource) {
    return $resource('api/users/:userId', { userId: '@userId' },
      {
        'checkDuplicateUnit': {
          method: 'GET',
          url: 'api/user/duplicateUnit',
          params: {
            userIdL: '',
            unitId: ''
          }
        }
      });
  }]);
}(angular));
