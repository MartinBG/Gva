/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function CommonScannedDocCtrl($scope, $stateParams) {
    $scope.lotId = $stateParams.id;

    if (_.isArray($scope.model)) {
      $scope.hideApplications = false;
    }
    else {
      $scope.hideApplications = $scope.model.hideApplications;
    }

    $scope.$watch('model', function () {
      if (!_.isArray($scope.model)) {
        $scope.model = $scope.model.files;
      }

      angular.forEach($scope.model, function (file) {
        file.isDeleted = false;
        file.isAdded = false;
      });
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

  CommonScannedDocCtrl.$inject = ['$scope', '$stateParams'];

  angular.module('gva').controller('CommonScannedDocCtrl', CommonScannedDocCtrl);
}(angular, Select2, _));
