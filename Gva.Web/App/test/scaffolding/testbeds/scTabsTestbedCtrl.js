/*global angular*/
(function (angular) {
  'use strict';

  function ScTabsCtrl($scope, $state) {
    $scope.state = '';

    $scope.$watch('state', function (newState) {
      if ($state.$current.name !== newState) {
        if (newState === '') {
          $state.go('root.scaffoldingTestbed.sctabs');
        }
        else {
          $state.go(newState);
        }
      }
    });

    $scope.$on('$stateChangeSuccess', function (event, toState) {
      $scope.state = toState.name;
    });
  }

  ScTabsCtrl.$inject = ['$scope', '$state'];

  angular.module('scaffolding').controller('ScTabsTestbedCtrl', ScTabsCtrl);
}(angular));