// Usage:
//<sc-navigation user-fullname="" change-password-state="">
//</sc-navigation>

/*global angular*/
(function (angular) {
  'use strict';

  function NavigationDirective(authService, $window) {
    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/navigation/navigationDirective.html',
      scope: {
        userFullname: '@',
        changePasswordState: '@'
      },
      controller: ['$scope', function NavigationCtrl($scope) {
        $scope.logout = function logout() {
          return authService.signOut().then(function () {
            $window.location.reload();
          });
        };
      }]
    };
  }
  NavigationDirective.$inject = ['authService', '$window'];
  angular.module('scaffolding').directive('scNavigation', NavigationDirective);
}(angular));
