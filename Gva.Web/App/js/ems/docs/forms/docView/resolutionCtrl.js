/*global angular*/
(function (angular) {
  'use strict';

  function ResolutionCtrl($scope) {
    $scope.requireDocUnitsFrom = function () {
      return $scope.model.docUnitsFrom.length > 0;
    };

    $scope.requireDocUnitsInCharge = function () {
      return $scope.model.docUnitsInCharge.length > 0;
    };

    $scope.requireAssignmentDeadline = function () {
      if ($scope.model.assignmentType) {
        if ($scope.model.assignmentType.alias === 'WithDeadline') {
          return !!$scope.model.assignmentDeadline;
        }
      }

      return true;
    };
  }

  ResolutionCtrl.$inject = ['$scope'];

  angular.module('ems').controller('ResolutionCtrl', ResolutionCtrl);
}(angular));
