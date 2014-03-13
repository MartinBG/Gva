/*global angular*/
(function (angular) {
  'use strict';

  function DocInternalOutgoingCtrl($scope) {
    $scope.requireDocUnitsFrom = function () {
      return $scope.model.docUnitsFrom.length > 0;
    };
    $scope.requireCorrs = function () {
      return $scope.model.docCorrespondents.length > 0;
    };
  }

  DocInternalOutgoingCtrl.$inject = ['$scope'];

  angular.module('ems').controller('DocInternalOutgoingCtrl', DocInternalOutgoingCtrl);
}(angular));
