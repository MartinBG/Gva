/*global angular, Select2, _*/
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
    $scope.hideApplications = scFormParams.hideApplications;

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

    $scope.$watch('model', function () {
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

        for (var i = 0; i < $scope.model.length; i++) {
          if ($scope.model[i] !== file &&
            $scope.model[i].caseType &&
            $scope.model[i].caseType.nomValueId === file.caseType.nomValueId &&
            $scope.model[i].bookPageNumber &&
            $scope.model[i].bookPageNumber === file.bookPageNumber
          ) {
            return false;
          }
        }

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
      };
    };

    $scope.getBPN = function (changedFile) {
      if (!changedFile.caseType || changedFile.bookPageNumber) {
        return;
      }

      var getIntRegExp = /^(\d+)/,
          currentBPNs = _($scope.model)
            .filter(function (file) {
              return file.caseType &&
                file.bookPageNumber &&
                file.caseType.nomValueId === changedFile.caseType.nomValueId &&
                getIntRegExp.test(file.bookPageNumber);
            })
            .map(function (file) {
              return parseInt(getIntRegExp.exec(file.bookPageNumber)[0], 10);
            })
            .value();

      var currentNextBPN = null;
      if (currentBPNs.length) {
        currentNextBPN = _(currentBPNs).max() + 1;
      }

      GvaParts.getNextBPN({
        lotId: scFormParams.lotId,
        caseTypeId: changedFile.caseType.nomValueId
      })
      .$promise
      .then(function (data) {
        changedFile.bookPageNumber = Math.max(data.nextBPN, currentNextBPN).toString();
      });
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
