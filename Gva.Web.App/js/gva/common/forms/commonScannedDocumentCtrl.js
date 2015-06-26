/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CommonScannedDocCtrl(
    $scope,
    Nomenclatures,
    scFormParams
  ) {
    var isMultipleMode = _.isArray($scope.model);

    $scope.lotId = scFormParams.lotId;
    $scope.hideApplications = scFormParams.hideApplications;
    $scope.hideCaseTypeData = scFormParams.hideCaseTypeData;
    $scope.hideFiles = scFormParams.hideFiles;
    $scope.showNote = scFormParams.showNote;
    $scope.hideTitle = scFormParams.hideTitle;

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
  }

  CommonScannedDocCtrl.$inject = [
    '$scope',
    'Nomenclatures',
    'scFormParams'
  ];

  angular.module('gva').controller('CommonScannedDocCtrl', CommonScannedDocCtrl);
}(angular, _));
