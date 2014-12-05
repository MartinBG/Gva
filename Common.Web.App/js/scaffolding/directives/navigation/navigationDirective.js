// Usage:
//<sc-navigation user-fullname="">
//</sc-navigation>

/*global angular*/
(function (angular) {
  'use strict';

  function NavigationDirective(authService, $window, scModal) {
    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/navigation/navigationDirective.html',
      scope: {
        userFullname: '@'
      },
      controller: ['$scope', function NavigationCtrl($scope) {
        $scope.logout = function logout() {
          return authService.signOut().then(function () {
            $window.location.reload();
          });
        };

        $scope.changePassword = function () {
          var modalInstance = scModal.open('changePassword');

          return modalInstance.opened;
        };

      }]
    };
  }
  NavigationDirective.$inject = ['authService', '$window', 'scModal'];
  angular.module('scaffolding').directive('scNavigation', NavigationDirective);
}(angular));
