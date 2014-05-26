/*global angular*/
(function (angular) {
  'use strict';

  function DocNewCommonCtrl($scope) {
    $scope.docFormatTypeChange = function ($index) {
      $scope.model.doc.docFormatTypeId =
        $scope.model.docFormatTypes[$index].nomValueId;
      $scope.model.doc.docFormatTypeName =
        $scope.model.docFormatTypes[$index].name;
    };

    $scope.docCasePartTypeChange = function ($index) {
      $scope.model.doc.docCasePartTypeId =
        $scope.model.docCasePartTypes[$index].nomValueId;
      $scope.model.doc.docCasePartTypeName =
        $scope.model.docCasePartTypes[$index].name;
    };

    $scope.docDirectionChange = function ($index) {
      $scope.model.doc.docDirectionId =
        $scope.model.docDirections[$index].nomValueId;
      $scope.model.doc.docDirectionName =
        $scope.model.docDirections[$index].name;
    };
  }

  DocNewCommonCtrl.$inject = ['$scope'];

  angular.module('ems').controller('DocNewCommonCtrl', DocNewCommonCtrl);
}(angular));
