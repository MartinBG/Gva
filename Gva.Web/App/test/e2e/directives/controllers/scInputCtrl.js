﻿(function (angular) {
  'use strict';

  function ScInputCtrl($scope) {
    $scope.text = null;
    $scope.intNum = null;
    $scope.floatNum = null;
    $scope.date = null;

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
  
  ScInputCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScInputCtrl', ScInputCtrl);
}(angular));
