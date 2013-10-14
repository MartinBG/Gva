(function (angular) {
  'use strict';

  function SearchCtrl($scope, $state, $stateParams, usersStates, User, l10n) {
    $scope.filters = {
      username: $stateParams.username,
      fullname: $stateParams.fullname,
      showActive: $stateParams.showActive
    };

    $scope.btnActions = {
      'newUser': function () {
        $state.go(usersStates.newUser);
      },
      'search': function () {
        $state.go(usersStates.search, {
          username: $scope.filters.username,
          fullname: $scope.filters.fullname,
          showActive: $scope.filters.showActive && $scope.filters.showActive.id
        });
      }
    };

    $scope.select2opt = {
      allowClear: true,
      placeholder: ' ',
      data: [
        { id: 'true', text: l10n.get('users.search.onlyActive') },
        { id: 'false', text: l10n.get('users.search.onlyUnactive') }
      ]
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
  
  SearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'users.States', 'users.User', 'l10n'];

  angular.module('users').controller('users.SearchCtrl', SearchCtrl);
}(angular));
