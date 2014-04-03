/*global angular, _, alert, $*/
(function (angular, _, alert, $) {
  'use strict';

  function SingleFileModalCtrl(
    $scope,
    $q,
    $modalInstance,
    $exceptionHandler,
    l10n,
    scFilesConfig,
    modalValue,
    isReadonly) {
    var file = modalValue,
      pendingUpload;

    $scope.file = undefined;
    $scope.scFilesConfig = scFilesConfig;
    $scope.isReadonly = isReadonly;

    if (file) {
      $scope.file = {
        url: scFilesConfig.fileUrl + '?' + $.param({
          'fileKey': file.key,
          'fileName': file.name
        }),
        name: file.name
      };
    }

    $scope.cancel = function () {
      $modalInstance.dismiss();
    };

    $scope.ok = function () {
      var uploadedFile;
      if (pendingUpload) {
        $scope.isReadonly = true;

        $q.when(pendingUpload.submit()).then(function (data) {
          if (data.fileKey) {
            var file = pendingUpload.files[0];
            pendingUpload = undefined;

            uploadedFile = {
              name: file.name,
              relativePath: file.relativePath,
              key: data.fileKey
            };
          }
        })['catch'](function (error) {
          $exceptionHandler(error, l10n.get('scaffolding.scFiles.failAlert'));
        })['finally'](function () {
          $modalInstance.close(uploadedFile);
        });
      } else {
        $modalInstance.close();
      }
    };

    $scope.add = function (e, data) {
      var file;

      if ($scope.isReadonly) {
        return;
      }

      file = data.files[0];
      pendingUpload = data;

      $scope.$apply(function () {
        $scope.file = {
          name: file.name,
          url: undefined
        };
      });
    };

    $scope.remove = function () {
      $scope.file = undefined;
      pendingUpload = undefined;
    };
  }

  SingleFileModalCtrl.$inject = [
    '$scope',
    '$q',
    '$modalInstance',
    '$exceptionHandler',
    'l10n',
    'scFilesConfig',
    'modalValue',
    'isReadonly'
  ];

  angular.module('scaffolding').controller('SingleFileModalCtrl', SingleFileModalCtrl);
}(angular, _, alert, $));