/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CommonScannedDocCtrl(
    $scope,
    Nomenclatures,
    GvaParts,
    scFormParams
  ) {
    var isMultipleMode = _.isArray($scope.model);

    $scope.lotId = scFormParams.lotId;
    $scope.setPart = scFormParams.setPart;
    $scope.hideApplications = scFormParams.hideApplications;
    $scope.hideFiles = scFormParams.hideFiles;

    $scope.$watch('model', function () {
      if (isMultipleMode) {
        $scope.files = $scope.model;
      }
      else if (!isMultipleMode && $scope.model) {
        $scope.files = [$scope.model];
      }
    });

    $scope.showFile = function () {
      return function (file) {
        return !file.isDeleted;
      };
    };

    $scope.isUniqueBPN = function (file) {
      return function () {
        return GvaParts.isUniqueBPN({
          lotId: scFormParams.lotId,
          caseTypeId: file.caseType.nomValueId,
          bookPageNumber: file.bookPageNumber,
          fileId: file.lotFileId
        }).$promise.then(function (data) {
          return data.isUnique;
        });
      };
    };
  }

  CommonScannedDocCtrl.$inject = [
    '$scope',
    'Nomenclatures',
    'GvaParts',
    'scFormParams'
  ];

  angular.module('gva').controller('CommonScannedDocCtrl', CommonScannedDocCtrl);
}(angular, _));
