(function (angular) {
  'use strict';

  function BreadcrumbBarCtrl($scope, $state, navigationConfig) {
    $scope.getBreadcrumbBarStates = function(state) {
      var states = [];

      var lastState = state;
      while (lastState) {
        if (lastState.title) {
          states.push({
            url: $state.href(lastState),
            title: lastState.title
          });
        }

        if (!lastState.parent){
          break;
        }

        lastState = lastState.parent;
      }

      states.reverse();
      return states;
    };

    $scope.showBreadcrumbBar = navigationConfig.showBreadcrumbBar;
    $scope.breadcrumbBarStates = [];

    if ($scope.showBreadcrumbBar) {
      $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates($state.current);
      $scope.$on('$stateChangeSuccess',
        function(event, toState){
          $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates(toState);
        });
    }
  }

  BreadcrumbBarCtrl.$inject = [ '$scope', '$state', 'navigation.NavigationConfig'];

  angular.module('navigation').controller('navigation.BreadcrumbBarCtrl', BreadcrumbBarCtrl);
}(angular));
