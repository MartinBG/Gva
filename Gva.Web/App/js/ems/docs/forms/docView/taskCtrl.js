/*global angular*/
(function (angular) {
  'use strict';

  function TaskCtrl($scope) {
    $scope.requireDocUnitsFrom = function () {
      return $scope.model.docUnitsFrom.length > 0;
    };
    $scope.requireDocUnitsInCharge = function () {
      return $scope.model.docUnitsInCharge.length > 0;
    };
  }

  TaskCtrl.$inject = ['$scope'];

  angular.module('ems').controller('TaskCtrl', TaskCtrl);
}(angular));
