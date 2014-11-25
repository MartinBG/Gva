/*global angular*/
(function (angular) {
  'use strict';

  function ChangePasswordModalCtrl(
    $scope,
    $modalInstance,
    Users
  ) {
    $scope.form = {};
    $scope.passwords = {};

    $scope.save = function () {
      return $scope.form.changePasswordForm.$validate().then(function () {
        if ($scope.form.changePasswordForm.$valid) {
          return Users.changePassword($scope.passwords).$promise.then(function () {
            return $modalInstance.close();
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.matchPasswords = function (confirmNewPassword) {
      if (!$scope.passwords.newPassword) {
        return true;
      }
      return $scope.passwords.newPassword === confirmNewPassword;
    };

    $scope.checkPassword = function (password) {
      if (!password) {
        return true;
      }
      return Users.isCorrectPassword(password).$promise
      .then(function (result) {
        return result.isCorrect;
      });
    };
  }

  ChangePasswordModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Users'
  ];

  angular.module('common').controller('ChangePasswordModalCtrl', ChangePasswordModalCtrl);
}(angular));