// Usage:
//<sc-navigation user-fullname="" change-password-state="" logout-state="">
//</sc-navigation>

/*global angular*/
(function (angular) {
  'use strict';

  function NavigationDirective(authService) {
    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/navigation/navigationDirective.html',
      scope: {
        userFullname: '@',
        changePasswordState: '@',
        logoutState: '@'
      },
      controller: function NavigationCtrl($scope, $state) {
        $scope.changePassword = function changePassword() {
          return $state.go($scope.changePasswordState);
        };

        $scope.logout = function logout() {
          authService.signOut();
          return $state.go($scope.logoutState);
        };
      }
    };
  }
  NavigationDirective.$inject = ['authService'];
  angular.module('scaffolding').directive('scNavigation', NavigationDirective);
}(angular));
