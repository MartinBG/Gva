(function (angular) {
  'use strict';

  function ScInputCtrl($scope) {
    $scope.text = null;
    $scope.intNum = null;
    $scope.floatNum = null;
  }
  
  ScInputCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScInputCtrl', ScInputCtrl);
}(angular));
