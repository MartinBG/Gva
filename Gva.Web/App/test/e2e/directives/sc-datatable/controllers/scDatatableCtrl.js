(function (angular) {
  'use strict';

  function ScDatatableCtrl($scope, $state, $stateParams, usersStates, User) {
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

        $scope.users2 = $scope.users;
      });

      $scope.loadMany = function(){
        for(var i = 0; i < 10; i++){
          $scope.users = $scope.users.concat($scope.users);
          $scope.users2 = $scope.users2.concat($scope.users2);
        }
      };

      $scope.editUser = function (user) {
        $state.go(usersStates.edit, { userId: user.userId });
      };
    }

  ScDatatableCtrl.$inject = ['$scope', '$state', '$stateParams', 'users.States', 'users.User'];

  angular.module('directive-tests').controller('directive-tests.ScDatatableCtrl', ScDatatableCtrl);
}(angular));
