/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function CommonScannedDocCtrl($scope, $stateParams, Nomenclature) {
    var caseType = null;
    $scope.lotId = $stateParams.id;

    var addNewFile = function () {
      $scope.model.unshift({
        caseType: caseType,
        file: null,
        bookPageNumber: null,
        pageCount: null,
        isAdded: true,
        isDeleted: false
      });
    };

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
        file.isAdded = file.isAdded || false;
      });
    });

    $scope.addFile = function () {
      if ($stateParams.caseTypeId && !caseType) {
        return Nomenclature.get({
          alias: 'organizationCaseTypes',
          id: $stateParams.caseTypeId
        }).$promise.then(function (ct) {
          caseType = ct;
          addNewFile();
        });
      }
      else {
        addNewFile();
      }
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

  CommonScannedDocCtrl.$inject = ['$scope', '$stateParams', 'Nomenclature'];

  angular.module('gva').controller('CommonScannedDocCtrl', CommonScannedDocCtrl);
}(angular, Select2, _));
