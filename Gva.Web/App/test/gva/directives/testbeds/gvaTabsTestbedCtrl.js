/*global angular*/
(function (angular) {
  'use strict';

  function GvaTabsCtrl($scope, $state) {
    $scope.state = '';

    $scope.$watch('state', function (newState) {
      if ($state.$current.name !== newState) {
        if (newState === '') {
          $state.go('root.scaffoldingTestbed.gvatabs');
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

  GvaTabsCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('GvaTabsTestbedCtrl', GvaTabsCtrl);
}(angular));