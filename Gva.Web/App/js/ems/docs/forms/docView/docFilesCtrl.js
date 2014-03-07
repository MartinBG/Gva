/*global angular*/
(function (angular) {
  'use strict';

  function DocFilesCtrl(
    $scope
  ) {

    $scope.editDocFile = function editDocFile(target) {
      target.isInEdit = true;
      target.prevValues = {
        docFileKindId: target.docFileKindId,
        docFileKind: target.docFileKind,
        docFileTypeId: target.docFileTypeId,
        docFileType: target.docFileType,
        docFile: target.docFile,
        isNew: target.isNew,
        isDirty: target.isDirty,
        isDeleted: target.isDeleted,
        isInEdit: false
      };
    };

    $scope.saveDocFile = function saveDocFile(target, form) {
      form.$validate().then(function () {
        if (form.$valid) {
          target.isDirty = true;
          target.isInEdit = false;
          target.prevValues = undefined;
        }
      });
    };

    $scope.cancelDocFile = function cancelDocFile(target) {
      if (target.prevValues) {
        target.docFileKindId = target.prevValues.docFileKindId;
        target.docFileKind = target.prevValues.docFileKind;
        target.docFileTypeId = target.prevValues.docFileTypeId;
        target.docFileType = target.prevValues.docFileType;
        target.docFile = target.prevValues.docFile;
        target.isNew = target.prevValues.isNew;
        target.isDirty = target.prevValues.isDirty;
        target.isDeleted = target.prevValues.isDeleted;
        target.isInEdit = target.prevValues.isInEdit;

        target.prevValues = undefined;
      } else {
        if (target.isNew) {
          target.isDeleted = true;
        }
        else {
          target.isInEdit = false;
        }
      }
    };

    $scope.removeDocFile = function removeDocFile(target) {
      target.isDeleted = true;
    };

    $scope.addDocFile = function addDocFile(id) {
      var docFile = {
        docId: id,
        docFileKindId: undefined,
        docFileKind: {},
        docFileTypeId: undefined,
        docFileType: {},
        docFile: undefined,
        isNew: true,
        isDirty: false,
        isDeleted: false,
        isInEdit: true
      };

      $scope.model.docFiles = $scope.model.docFiles || [];
      $scope.model.docFiles.push(docFile);
    };
  }

  DocFilesCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocFilesCtrl', DocFilesCtrl);
}(angular));
