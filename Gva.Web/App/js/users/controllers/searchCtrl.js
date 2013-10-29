/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SearchCtrl($scope, $state, $stateParams, usersStates, User) {
    $scope.filters = {
      username: null //always show the username filter
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newUser = function () {
      $state.go(usersStates.newUser);
    };

    $scope.search = function () {
      $state.go(usersStates.search, {
        username: $scope.filters.username,
        fullname: $scope.filters.fullname,
        showActive: $scope.filters.showActive && $scope.filters.showActive.id
      });
    };

    User.query($stateParams).$promise.then(function (users) {
      $scope.users = users.map(function (user) {
        var roles = '';
        for (var i = 0; i < user.roles.length; i++) {
          roles += user.roles[i].name + ', ';
        }
        roles = roles.substring(0, roles.length - 2);

        return {
          userId: user.userId,
          username: user.username,
          fullname: user.fullname,
          roles: roles,
          isActive: user.isActive
        };
      });
    });

    $scope.editUser = function (user) {
      $state.go(usersStates.edit, { userId: user.userId });
    };
  }
  
  SearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'users.States', 'users.User'];

  angular.module('users').controller('users.SearchCtrl', SearchCtrl);
}(angular, _));
