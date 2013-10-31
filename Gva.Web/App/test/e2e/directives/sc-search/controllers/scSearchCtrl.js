(function (angular) {
  'use strict';

  function ScSearchCtrl($scope) {
    $scope.filters = {
      filter1: null,
      filter3: null
    };

    $scope.changeInt = function () {
      $scope.intNum = 789;
    };

    $scope.changeFloat = function () {
      $scope.floatNum = 789.12;
    };

    $scope.changeDate = function () {
      $scope.date = '1990-08-10T12:18:12';
    };
  }
  
  ScSearchCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScSearchCtrl', ScSearchCtrl);
}(angular));
