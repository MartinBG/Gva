/*global angular, $*/
(function (angular, $) {
  'use strict';

  function FilesCtrl($scope, $modal, $interpolate, l10n, scFilesConfig) {
    $scope.openModal = function () {
      var modal = $modal.open({
        templateUrl: $scope.isMultiple ?
          'js/scaffolding/directives/files/filesModal.html' :
          'js/scaffolding/directives/files/singleFileModal.html',
        controller: $scope.isMultiple ?
          'FilesModalCtrl' : 'SingleFileModalCtrl',
        backdrop: 'static',
        keyboard: false,
        resolve: {
          modalValue: function () {
            return $scope.getViewValue();
          },
          isReadonly: function () {
            return $scope.isReadonly;
          }
        }
      });
      modal.result.then(function (files) {
        $scope.setViewValue(files);
        $scope.update(files);
      });
    };

    $scope.update = function (files) {
      var file;
      $scope.noFiles = !files || files.length === 0;
      $scope.singleFile = files && (!$scope.isMultiple || files.length === 1);

      if ($scope.noFiles) {
        $scope.uploadedFilesText = $scope.isMultiple ?
          $scope.uploadedFilesText = l10n.get('scaffolding.scFiles.noFiles') :
          l10n.get('scaffolding.scFiles.noFile');
      } else if ($scope.singleFile) {
        file = $scope.isMultiple ? files[0] : files;
        $scope.fileUrl =
          scFilesConfig.fileUrl + '?' +
          $.param({
            'fileKey': file.key,
            'fileName': file.name,
            'mimeType': file.mimeType
          });
        $scope.uploadedFilesText = file.name;
      } else {
        $scope.uploadedFilesText =
          $interpolate(l10n.get('scaffolding.scFiles.manyFiles'))({
            filesCount: files.length
          });
      }
    };
  }

  FilesCtrl.prototype.setNgModelCtrl = function (ngModel, $scope) {
    $scope.getViewValue = function () {
      return ngModel.$viewValue;
    };
    $scope.setViewValue = function (value) {
      ngModel.$setViewValue(value);
    };

    ngModel.$render = function () {
      $scope.update(ngModel.$viewValue);
    };
  };

  FilesCtrl.$inject = ['$scope', '$modal', '$interpolate', 'l10n', 'scFilesConfig'];

  angular.module('scaffolding').controller('FilesCtrl', FilesCtrl);
}(angular, $));
