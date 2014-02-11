/*global angular*/
(function (angular) {
  'use strict';

  function BreadcrumbBarCtrl($scope, $state, l10n, navigationConfig) {
    $scope.getBreadcrumbBarStates = function(state) {
      var states = [];

      while (state) {
        var title = l10n.states[state.self.name];
        if (title) {
          states.push({
            url: $state.href(state.self),
            title: title
          });
        }

        state = state.parent;
      }

      states.reverse();
      return states;
    };

    $scope.showBreadcrumbBar = navigationConfig.showBreadcrumbBar;
    $scope.breadcrumbBarStates = [];
    $scope.rootUrl = $state.href(navigationConfig.breadcrumbBarHomeState);

    if ($scope.showBreadcrumbBar) {
      $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates($state.$current);
      $scope.$on('$stateChangeSuccess', function(){
        $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates($state.$current);
      });
    }
  }

  BreadcrumbBarCtrl.$inject = ['$scope', '$state', 'l10n', 'NavigationConfig'];

  angular.module('common').controller('BreadcrumbBarCtrl', BreadcrumbBarCtrl);
}(angular));
