(function (angular) {
  'use strict';
    

  function BreadcrumbBarCtrl($rootScope, $scope, $state, navigationConfig) {
    $scope.updateBreadcrumbBarStates = function(state) {
      var states = [];

      var lastState = state;
      while(lastState){
        if(lastState.title){
          states.push(lastState);
        }

        if(!lastState.parent){
          break;
        }

        lastState = lastState.parent;
      }

      states.reverse();
      return states;
    };

    $scope.showBreadcrumbBar = navigationConfig.showBreadcrumbBar;
    $scope.breadcrumbBarStates = [$state.current];
    $scope.breadcrumbBarStates = $scope.updateBreadcrumbBarStates($state.current);

    if($scope.showBreadcrumbBar) {
      $rootScope.$on('$stateChangeSuccess',
          function(event, toState){
            $scope.breadcrumbBarStates = $scope.updateBreadcrumbBarStates(toState);
          });
    }
  }

  BreadcrumbBarCtrl.$inject = [ '$rootScope', '$scope', '$state', 'navigation.NavigationConfig'];

  angular.module('navigation').controller('navigation.BreadcrumbBarCtrl', BreadcrumbBarCtrl);
}(angular));
