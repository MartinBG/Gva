/*global angular, _*/
(function (angular, _) {
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

        if (_.isString(lastState.parent)) {
          lastState = $state.get(lastState.parent);
        } else {
          lastState = lastState.parent;
        }
      }

      states.reverse();
      return states;
    };

    $scope.showBreadcrumbBar = navigationConfig.showBreadcrumbBar;
    $scope.breadcrumbBarStates = [];
    $scope.rootUrl = $state.href(navigationConfig.breadcrumbBarHomeState);

    if ($scope.showBreadcrumbBar) {
      $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates($state.current);
      $scope.$on('$stateChangeSuccess',
        function(event, toState){
          $scope.breadcrumbBarStates = $scope.getBreadcrumbBarStates(toState);
        });
    }
  }

  BreadcrumbBarCtrl.$inject = [ '$scope', '$state', 'NavigationConfig'];

  angular.module('common').controller('BreadcrumbBarCtrl', BreadcrumbBarCtrl);
}(angular, _));
