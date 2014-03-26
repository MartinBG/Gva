/*global angular*/
(function (angular) {
  'use strict';

  function LoginModalCtrl($scope, $modalInstance, authService) {
    $scope.invalidUsernameAndPassword = false;
    $scope.login = function (loginForm) {
      return loginForm
        .$validate()
        .then(function () {
          if (loginForm.$valid) {
            return authService
              .authenticate(loginForm.username.$modelValue, loginForm.password.$modelValue)
              .then(function (success) {
                if (success) {
                  $modalInstance.close();
                } else {
                  $scope.invalidUsernameAndPassword = true;
                }
              });
          }
        });
    };
  }

  LoginModalCtrl.$inject = ['$scope', '$modalInstance', 'authService'];

  angular.module('common').controller('LoginModalCtrl', LoginModalCtrl);
}(angular));
