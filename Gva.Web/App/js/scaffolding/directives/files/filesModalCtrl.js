/*global angular, _, alert, $*/
(function (angular, _, alert, $) {
  'use strict';

  function FilesModalCtrl($scope,
    $q,
    $interpolate,
    $modalInstance,
    scFilesConfig,
    files,
    isReadonly) {
    var pendingUploads = {},
      uploadedFiles = {},
      canceled;

    $scope.isInEdit = true;
    $scope.fileHierarchy = [];
    $scope.filesUploaded = 0;
    $scope.filesLength = 0;
    $scope.scFilesConfig = scFilesConfig;
    $scope.isReadonly = isReadonly;

    (files || []).forEach(function (file) {
      var key = file.relativePath + file.name + ';' + file.key;
      uploadedFiles[key] = file;
      addToHierarchy(
        {
          pending: false,
          key: key,
          url: scFilesConfig.fileUrl + '?' + $.param({
            'fileKey': file.key,
            'fileName': file.name
          })
        },
        file.name,
        file.relativePath);
    });

    $scope.cancel = function () {
      $modalInstance.dismiss();
    };

    $scope.ok = function () {
      var keys = _.keys(pendingUploads);

      function uploadNext() {
        var nextKey;
        if (!keys.length || canceled) {
          return $q.when(undefined);
        }

        nextKey = keys.shift();
        return $q.when(pendingUploads[nextKey].submit()).then(function (data) {
          if (data.fileKey) {
            var file = pendingUploads[nextKey].files[0],
                key = file.relativePath + file.name + ';' + data.fileKey;
            uploadedFiles[key] = {
              name: file.name,
              relativePath: file.relativePath,
              key: data.fileKey
            };
            delete pendingUploads[nextKey];
            $scope.filesUploaded = $scope.filesUploaded + 1;
            return uploadNext();
          }
        });
      }

      $scope.isInEdit = false;
      $scope.isUploading = true;
      $scope.filesUploaded = 0;
      $scope.filesLength = keys.length;

      uploadNext()['catch'](function () {
        alert('Възникна грешка. Успешно качените файлове са записани. Опитайте отново.');
      })['finally'](function () {
        $scope.isUploading = false;
        $scope.filesUploaded = 0;
        $scope.filesLength = 0;
        $modalInstance.close(_.values(uploadedFiles));
      });
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

      $scope.$apply(function () {
        if (addToHierarchy(item, file.name, file.relativePath)) {
          pendingUploads[key] = data;
        }
      });
    };

    $scope.remove = function (node) {
      var item = node.item;

      if (item) {
        delete pendingUploads[item.key];
        delete uploadedFiles[item.key];
      }

      if (node.children) {
        //iterate over a copy as removing from inside the forEach
        //leads to unexpected results
        node.children.slice().forEach(function (node) {
          $scope.remove(node);
        });
        //the current node will be remove after all of its children are removed
      } else {
        removeFromHierarchy(node);
      }
    };

    $scope.stop = function () {
      canceled = true;
    };

    function sortByName(a, b) {
      if (a.name < b.name) {
        return -1;
      }
      if (a.name > b.name) {
        return 1;
      }
      return 0;
    }

    function addToHierarchy(item, name, relativePath, parent) {
      var parentChildren = (parent && parent.children) || $scope.fileHierarchy,
          firstSlash,
          folder,
          rest,
          node;

      if (relativePath) {
        firstSlash = Math.max(relativePath.indexOf('\\'), relativePath.indexOf('\/'));
        if (firstSlash !== -1) {
          folder = relativePath.substr(0, firstSlash + 1);
          rest = relativePath.substr(firstSlash + 1);
        } else {
          folder = relativePath;
          rest = undefined;
        }

        node = parentChildren.filter(function (node) {
          return node.name === folder && node.children;/*a folder item*/
        })[0];


        if (!node) {
          node = {
            name: folder,
            parent: parent,
            children: [],
            item: undefined,
            canRemove: $scope.isInEdit,
            isExpanded: true
          };
          parentChildren.push(node);
          parentChildren.sort(sortByName);
        }

        return addToHierarchy(item, name, rest, node);
      } else {
        node = parentChildren.filter(function (node) {
          return node.name === name;
        })[0];

        if (node) {
          return false;
        }

        parentChildren.push({
          name: name,
          parent: parent,
          children: undefined,
          item: item,
          canRemove: $scope.isInEdit,
          isExpanded: undefined
        });

        parentChildren.sort(sortByName);
        return true;
      }
    }

    function removeFromHierarchy(node) {
      var parentChildren = (node.parent && node.parent.children) || $scope.fileHierarchy,
          nodeIndex = parentChildren.indexOf(node);

      if (nodeIndex !== -1) {
        parentChildren.splice(nodeIndex, 1);
      }

      if (node.parent && parentChildren.length === 0) {
        removeFromHierarchy(node.parent);
      }

      node.parent = undefined;
    }
  }

  FilesModalCtrl.$inject = [
    '$scope',
    '$q',
    '$interpolate',
    '$modalInstance',
    'scFilesConfig',
    'files',
    'isReadonly'
  ];

  angular.module('scaffolding').controller('FilesModalCtrl', FilesModalCtrl);
}(angular, _, alert, $));