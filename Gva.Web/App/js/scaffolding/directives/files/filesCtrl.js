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
        templateUrl: 'scaffolding/directives/files/filesModal.html',
        controller: 'scaffolding.FilesModalCtrl',
        backdrop: 'static',
        keyboard: false,
        resolve: {
          files: function () {
            return self.ngModel.$viewValue;
          },
          isReadonly: function () {
            return $scope.isReadonly;
          }
        }
      });
      modal.result.then(function (files) {
        self.ngModel.$setViewValue(files);
        self.updateScope(files, $scope);
      });
    };
  }

  FilesCtrl.prototype.setNgModelCtrl = function (ngModel) {
    var self = this;

    this.ngModel = ngModel;
    ngModel.$render = function () {
      self.updateScope(ngModel.$viewValue, self.$scope);
    };
  };

  FilesCtrl.prototype.updateScope = function (files, $scope) {
    var self = this;
    $scope.singleFile = false;
    $scope.noFiles = false;
    if (files.length === 0) {
      $scope.noFiles = true;
      $scope.uploadedFilesText = self.l10n.get('scaffolding.scFiles.noFiles');
    } else if (files.length === 1) {
      $scope.singleFile = true;
      $scope.fileUrl =
        self.scFilesConfig.fileUrl + '?' +
        $.param({
          'fileKey': files[0].key,
          'fileName': files[0].name
        });
      $scope.uploadedFilesText = files[0].name;
    } else {
      $scope.uploadedFilesText =
        self.$interpolate(
          self.l10n.get('scaffolding.scFiles.manyFiles'))({ filesCount: files.length });
    }
  };

  FilesCtrl.$inject = ['$scope', '$modal', '$interpolate', 'l10n', 'scFilesConfig'];

  angular.module('scaffolding').controller('scaffolding.FilesCtrl', FilesCtrl);
}(angular, $));
