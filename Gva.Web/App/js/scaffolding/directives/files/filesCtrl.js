/*global angular, $*/
(function (angular, $) {
  'use strict';

  function FilesCtrl($scope, $modal, $interpolate, l10n, scFilesConfig) {
    var self = this;

    self.$scope = $scope;
    self.$interpolate = $interpolate;
    self.l10n = l10n;
    self.scFilesConfig = scFilesConfig;

    $scope.openModal = function () {
      var modal = $modal.open({
        templateUrl: $scope.isMultiple ?
          'scaffolding/directives/files/filesModal.html' :
          'scaffolding/directives/files/singleFileModal.html',
        controller: $scope.isMultiple ?
          'FilesModalCtrl' : 'SingleFileModalCtrl',
        backdrop: 'static',
        keyboard: false,
        resolve: {
          files: function () {
            return self.ngModel.$viewValue;
          },
          file: function () {
            return self.ngModel.$viewValue;
          },
          isReadonly: function () {
            return $scope.isReadonly;
          }
        }
      });
      modal.result.then(function (files) {
        self.ngModel.$setViewValue(files);
        self.updateScope(files, $scope, $scope.isMultiple);
      });
    };
  }

  //  $scope.openSingleFileModal = function () {
  //    var modal = $modal.open({
  //      templateUrl: 'scaffolding/directives/files/singleFileModal.html',
  //      controller: 'SingleFileModalCtrl',
  //      backdrop: 'static',
  //      keyboard: false,
  //      resolve: {
  //        file: function () {
  //          return self.ngModel.$viewValue;
  //        },
  //        isReadonly: function () {
  //          return $scope.isReadonly;
  //        }
  //      }
  //    });
  //    modal.result.then(function (files) {
  //      self.ngModel.$setViewValue(files);
  //      self.updateScope(files, $scope, false);
  //    });
  //  };
  //}



  FilesCtrl.prototype.setNgModelCtrl = function (ngModel, isMultiple) {
    var self = this;

    this.ngModel = ngModel;
    ngModel.$render = function () {
      self.updateScope(ngModel.$viewValue, self.$scope, isMultiple);
    };
  };

  FilesCtrl.prototype.updateScope = function (files, $scope, isMultiple) {
    var self = this,
      file;
    $scope.noFiles = !files || files.length === 0;
    $scope.singleFile = files && (!isMultiple || files.length === 1);

    if ($scope.noFiles) {
      $scope.uploadedFilesText = isMultiple ?
        $scope.uploadedFilesText = self.l10n.get('scaffolding.scFiles.noFiles') :
        self.l10n.get('scaffolding.scFiles.noFile');
    } else if ($scope.singleFile) {
      file = files[0] || files;
      $scope.fileUrl =
        self.scFilesConfig.fileUrl + '?' +
        $.param({
          'fileKey': file.key,
          'fileName': file.name
        });
      $scope.uploadedFilesText = file.name;
    } else {
      $scope.uploadedFilesText =
        self.$interpolate(self.l10n.get('scaffolding.scFiles.manyFiles'))({
          filesCount: files.length
        });
    }
  };

  FilesCtrl.$inject = ['$scope', '$modal', '$interpolate', 'l10n', 'scFilesConfig'];

  angular.module('scaffolding').controller('FilesCtrl', FilesCtrl);
}(angular, $));
