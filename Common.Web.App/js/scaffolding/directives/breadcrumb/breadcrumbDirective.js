// Usage:
//<sc-breadcrumb home-state="homeState"></sc-breadcrumb>

/*global angular*/
(function (angular) {
  'use strict';

  function BreadcrumbDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      scope: {
        homeState: '@'
      },
      templateUrl: 'js/scaffolding/directives/breadcrumb/breadcrumbDirective.html',
      controller: ['$scope', '$state', 'l10n', function BreadcrumbCtrl($scope, $state, l10n) {
        $scope.getBreadcrumbStates = function (state) {
          var states = [];

          while (state) {
            var title = l10n.states[state.self.name];
            if (title) {
              states.push({
                state: state['abstract'] ? state.defaultChild : state,
                title: title
              });
            }

            state = state.parent;
          }

          states.reverse();
          return states;
        };

        $scope.breadcrumbStates = $scope.getBreadcrumbStates($state.$current);
        $scope.$on('$stateChangeSuccess', function () {
          $scope.breadcrumbStates = $scope.getBreadcrumbStates($state.$current);
        });
      }]
    };
  }

  angular.module('scaffolding')
    .directive('scBreadcrumb', BreadcrumbDirective);
}(angular));
