/*global angular, _*/
(function (angular, _) {
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
        },
        changePassword: {
          method: 'POST',
          url: 'api/user/changePassword'
        },
        isCorrectPassword: {
          method: 'POST',
          url: 'api/user/isCorrectPassword',
          transformRequest: function (data, headers) {
            _.extend(headers(), {
              'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            });
            return encodeURIComponent('password') + '=' + encodeURIComponent(data);
          }
        }
      });
  }]);
}(angular, _));
