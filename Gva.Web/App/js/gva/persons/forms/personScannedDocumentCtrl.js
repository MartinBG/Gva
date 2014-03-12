/*global angular*/
(function (angular) {
  'use strict';

  function PersonScannedDocCtrl($scope) {
    if ($scope.model === undefined) {
      $scope.model = [];
    }

    angular.forEach($scope.model, function (file) {
      file.isDeleted = false;
      file.isAdded = false;
    });

    $scope.addFile = function () {
      $scope.model.unshift({
        file: null,
        bookPageNumber: null,
        pageCount: null,
        isAdded: true,
        isDeleted: false
      });
    };

    $scope.removeFile = function (index) {
      if ($scope.model[index].isAdded === true) {
        $scope.model.splice(index, 1);
      }
      else {
        $scope.model[index].isDeleted = true;
      }
    };

    $scope.showFile = function () {
      return function (file) {
        return !file.isDeleted;
      };
    };
  }

  PersonScannedDocCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonScannedDocCtrl', PersonScannedDocCtrl);
}(angular));
