/*global angular*/
(function (angular) {
  'use strict';
  
  function EditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    usersStates,
    User,
    Role
  ) {
    var userExistsPromise;

    if ($stateParams.userId) {
      $scope.isEdit = true;
      $scope.user = User.get({userId: $stateParams.userId});
    } else {
      $scope.isEdit = false;
      $scope.user = new User();
      $scope.user.$promise = $q.when($scope.user);
    }
    $scope.roles = Role.query();
    $scope.saveClicked = false;
    
    $q.all({
      user: $scope.user.$promise,
      roles: $scope.roles.$promise
    }).then(function (res) {
      res.roles.forEach(function (role) {
        role.selected =
          $filter('filter')(res.user.roles || [], {roleId: role.roleId}).length > 0;
      });
      
      $scope.setPassword = res.user.hasPassword;
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
        
        $scope.user.roles = $filter('filter')($scope.roles, {selected: true});
        
        if ($scope.isEdit) {
          userExistsPromise = $q.when(false);
        } else {
          userExistsPromise =
            User.query({username: $scope.user.username, exact: true})
            .$promise
            .then(function (users) {
              return users.length > 0;
            });
        }

        userExistsPromise.then(function (exists) {
          $scope.userExists = exists;
          if (!exists) {
            $scope.user.$save().then(function () {
              $state.go(usersStates.search);
            });
          }
        });
      }
    };

    $scope.cancel = function () {
      $state.go(usersStates.search);
    };
  }
  
  EditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'users.States',
    'users.User',
    'users.Role'
  ];
  
  angular.module('users').controller('users.EditCtrl', EditCtrl);
}(angular));
