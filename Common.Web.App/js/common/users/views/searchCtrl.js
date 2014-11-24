/*global angular, _*/
(function (angular, _) {
  'use strict';

  function UsersSearchCtrl($scope, $state, $stateParams, users) {
    $scope.users = users;

    $scope.filters = {
      username: null //always show the username filter
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newUser = function () {
      $state.go('root.users.new');
    };

    $scope.search = function () {
      $state.go('root.users.search', {
        username: $scope.filters.username,
        fullname: $scope.filters.fullname,
        showActive: $scope.filters.showActive
      });
    };

    $scope.editUser = function (user) {
      $state.go('root.users.edit', { userId: user.userId });
    };
  }

  UsersSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'users'];

  UsersSearchCtrl.$resolve = {
    users: [
      '$stateParams',
      'Users',
      function ($stateParams, Users) {
        return Users.query($stateParams).$promise.then(function (users) {
          return users.map(function (user) {
            return {
              userId: user.userId,
              username: user.username,
              fullname: user.fullname,
              roles: _.pluck(user.roles, 'name').join(', '),
              isActive: user.isActive
            };
          });
      });
      }
    ]
  };
  angular.module('common').controller('UsersSearchCtrl', UsersSearchCtrl);
}(angular, _));
