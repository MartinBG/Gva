(function (angular) {
  'use strict';

  function ScInputCtrl($scope) {
    $scope.text = null;
    $scope.intNum = null;
    $scope.floatNum = null;

    $scope.changeInt = function () {
      $scope.intNum = 789;
    };

    $scope.changeFloat = function () {
      $scope.floatNum = 789.12;
    };
  }
  
  ScInputCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScInputCtrl', ScInputCtrl);
}(angular));
