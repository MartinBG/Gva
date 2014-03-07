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
  }

  ResolutionCtrl.$inject = ['$scope'];

  angular.module('ems').controller('ResolutionCtrl', ResolutionCtrl);
}(angular));
