/*global angular*/
(function (angular) {
  'use strict';

  function ScSearchCtrl($scope) {
    $scope.filters = {
      filter1: null,
      filter2: null,
      filter3: null
    };

    $scope.btn1Clicks = 0;
    $scope.btn2Clicks = 0;

    $scope.action1 = function () {
      $scope.btn1Clicks++;
    };

    $scope.action2 = function () {
      $scope.btn2Clicks++;
    };
  }

  ScSearchCtrl.$inject = ['$scope'];

  angular.module('directive-tests').controller('directive-tests.ScSearchCtrl', ScSearchCtrl);
}(angular));
