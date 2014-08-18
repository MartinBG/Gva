// Usage:
//<sc-navigation-item active="" state="" text="" icon="" parent="" url="" new-tab="">
//</sc-navigation-item>

/*global angular*/
(function (angular) {
  'use strict';

  function NavigationItemDirective(l10n) {
    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      scope: {
        active: '@',
        state: '@',
        text: '@',
        icon: '@',
        parent: '@',
        url: '@',
        newTab: '@',
        params: '&'
      },
      templateUrl: 'js/scaffolding/directives/navigation/navigationItemDirective.html',
      controller: ['$scope', function NavigationItemCtrl($scope) {
        $scope.item = {
          active: $scope.active,
          parent: $scope.parent,
          state: $scope.state,
          icon: $scope.icon,
          text: l10n.get($scope.text),
          url: $scope.url,
          newTab: $scope.newTab
        };
      }]
    };
  }

  NavigationItemDirective.$inject = ['l10n'];

  angular.module('scaffolding').directive('scNavigationItem', NavigationItemDirective);
}(angular));
