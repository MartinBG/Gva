/*global angular*/
(function (angular) {
  'use strict';

  function DocOutgoingCtrl($scope) {
    $scope.requireDocUnitsFrom = function () {
      return $scope.model.docUnitsFrom.length > 0;
    };
    $scope.requireCorrs = function () {
      return $scope.model.docCorrespondents.length > 0;
    };
  }

  DocOutgoingCtrl.$inject = ['$scope'];

  angular.module('ems').controller('DocOutgoingCtrl', DocOutgoingCtrl);
}(angular));
