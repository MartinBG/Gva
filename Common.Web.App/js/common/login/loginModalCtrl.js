/*global angular,$*/
(function (angular, $) {
  'use strict';

  function LoginModalCtrl($scope, $modalInstance, $timeout, authService) {
    $scope.invalidUsernameAndPassword = false;
    $scope.submitting = false;

    $modalInstance.opened.then(function () {
      $timeout(function () {
        $('input[name$="username"]').focus();
        $('input[name$="username"], input[name$="password"]').checkAndTriggerAutoFillEvent();
      }, 200);
    });

    $scope.login = function (form, username, password) {
      return form
        .$validate()
        .then(function () {
          if (form.$valid && !$scope.submitting) {
            $scope.submitting = true;
            $scope.invalidUsernameAndPassword = false;

            return authService
              .authenticate(username, password)
              .then(function (success) {
                if (success) {
                  $modalInstance.close();
                } else {
                  $scope.submitting = false;
                  $scope.invalidUsernameAndPassword = true;
                }
              });
          }
        });
    };
  }

  LoginModalCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'authService'];

  angular.module('common').controller('LoginModalCtrl', LoginModalCtrl);
}(angular, $));
