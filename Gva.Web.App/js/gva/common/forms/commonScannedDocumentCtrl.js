﻿/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function CommonScannedDocCtrl(
    $scope,
    Nomenclatures,
    GvaParts,
    scFormParams
  ) {
    var caseType = null;
    $scope.lotId = scFormParams.lotId;
    $scope.setPart = scFormParams.setPart;

    var addNewFile = function () {
      $scope.model.push({
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
      if (scFormParams.caseTypeId && !caseType) {
        return Nomenclatures.get({
          alias: 'caseTypes',
          id: scFormParams.caseTypeId
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

    $scope.isUniqueBPN = function (file) {
      return function () { 
        if (!file.caseType || !file.bookPageNumber) {
          return true;
        }
        else {
          return GvaParts.isUniqueBPN({
            lotId: scFormParams.lotId,
            caseTypeId: file.caseType.nomValueId,
            bookPageNumber: file.bookPageNumber,
            fileId: file.lotFileId
          })
            .$promise
            .then(function (data) {
              return data.isUnique;
            });
        }

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
}(angular, Select2, _));
