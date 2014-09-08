/*global angular*/
(function (angular) {
  'use strict';

  function UsersEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    Users,
    Roles
  ) {
    var userExistsPromise,
      unitExistsPromise;

    if ($stateParams.userId) {
      $scope.isEdit = true;
      $scope.user = Users.get({ userId: $stateParams.userId });
    } else {
      $scope.isEdit = false;
      $scope.user = new Users();
      $scope.user.$promise = $q.when($scope.user);
    }
    $scope.roles = Roles.query();
    $scope.saveClicked = false;

    $q.all({
      user: $scope.user.$promise,
      roles: $scope.roles.$promise
    }).then(function (res) {
      res.roles.forEach(function (role) {
        role.selected =
          $filter('filter')(res.user.roles || [], { roleId: role.roleId }).length > 0;
      });

      $scope.setPassword = res.user.hasPassword || true;
      $scope.password = '';
      $scope.confirmPassword = '';

      $scope.setCertificate = !!res.user.certificateThumbprint;
      $scope.certificate = res.user.certificateThumbprint;
    });

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.userForm.$valid && $scope.password === $scope.confirmPassword) {
        if ($scope.setPassword) {
          $scope.user.password = $scope.password;
        } else {
          $scope.user.password = '';
        }

        if ($scope.setCertificate) {
          $scope.user.certificateThumbprint = $scope.certificate;
        } else {
          $scope.user.certificateThumbprint = '';
        }

        $scope.user.roles = $filter('filter')($scope.roles, { selected: true });

        if ($scope.isEdit) {
          userExistsPromise = $q.when(false);
        } else {
          userExistsPromise =
            Users.query({ username: $scope.user.username, exact: true })
            .$promise
            .then(function (users) {
              return users.length > 0;
            });
        }

        if ($scope.user.unitId) {
          unitExistsPromise =
            Users.checkDuplicateUnit({
              userId: $scope.user.userId,
              unitId: $scope.user.unitId
            })
          .$promise;
        } else {
          unitExistsPromise = $q.when(false);
        }

        $q.all({
          user1: userExistsPromise,
          unit1: unitExistsPromise
        }).then(function (res) {
          $scope.userExists = res.user1;
          $scope.unitExists = res.unit1.result;

          if (!$scope.userExists && !$scope.unitExists) {
            $scope.user.$save().then(function () {
              $state.go('root.users.search');
            });
          }
        });
      }
    };

    $scope.cancel = function () {
      $state.go('root.users.search');
    };
  }

  UsersEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Users',
    'Roles'
  ];

  angular.module('common').controller('UsersEditCtrl', UsersEditCtrl);
}(angular));
