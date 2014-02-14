/*global angular, _, alert, $*/
(function (angular, _, alert, $) {
  'use strict';

  function SingleFileModalCtrl($scope,
    $q,
    $interpolate,
    $modalInstance,
    scFilesConfig,
    file,
    isReadonly) {
    var pendingUpload,
      uploadedFile,
      canceled;

    $scope.isInEdit = true;
    $scope.fileHierarchy = [];
    $scope.filesUploaded = 0;
    $scope.filesLength = 0;
    $scope.file = undefined;
    $scope.scFilesConfig = scFilesConfig;
    $scope.isReadonly = isReadonly;

    if (file) {
      $scope.file = {
        item: {
          pending: false,
          key: file.name + ';' + file.key,
          url: scFilesConfig.fileUrl + '?' + $.param({
            'fileKey': file.key,
            'fileName': file.name
          })
        },
        canRemove: $scope.isInEdit,
        name: file.name
      };
    }

    $scope.cancel = function () {
      $modalInstance.dismiss();
    };

    var upload = function () {

      return $q.when(pendingUpload.submit()).then(function (data) {
        if (data.fileKey) {
          var pendingFile = pendingUpload.files[0];
          uploadedFile = {
            name: pendingFile.name,
            relativePath: pendingFile.relativePath,
            key: data.fileKey
          };
          pendingUpload = undefined;
        }
      });
    };

    $scope.ok = function () {
      if (pendingUpload) {
        $scope.isInEdit = false;

        upload()['catch'](function () {
          alert('Възникна грешка. Успешно качените файлове са записани. Опитайте отново.');
        })['finally'](function () {
          $scope.isUploading = false;
          $modalInstance.close(uploadedFile);
        });
      }
      else {
        $modalInstance.close();
      }
    };

    $scope.add = function (e, data) {
      var file,
          key,
          item;

      if (!$scope.isInEdit) {
        return;
      }

      file = data.files[0];
      key = 'f' + Math.floor(Math.random() * 100000000);
      item = { pending: true, key: key, url: undefined };
      pendingUpload = data;

      $scope.$apply(function () {
        $scope.file = {
          name: file.name,
          item: item,
          canRemove: $scope.isInEdit
        };
      });
    };

    $scope.remove = function () {
      $scope.file = undefined;
      pendingUpload = undefined;
    };

    $scope.stop = function () {
      canceled = true;
    };
  }

  SingleFileModalCtrl.$inject = [
    '$scope',
    '$q',
    '$interpolate',
    '$modalInstance',
    'scFilesConfig',
    'file',
    'isReadonly'
  ];

  angular.module('scaffolding').controller('SingleFileModalCtrl', SingleFileModalCtrl);
}(angular, _, alert, $));