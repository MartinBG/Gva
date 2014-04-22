/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocFilesEditCtrl($scope, Nomenclature) {
    $scope.editDocFile = function editDocFile(target) {
      target.isInEdit = true;
      target.prevValues = {
        docFileKindId: target.docFileKindId,
        docFileKind: target.docFileKind,
        docFileTypeId: target.docFileTypeId,
        docFileType: target.docFileType,
        file: target.file,
        isNew: target.isNew,
        isDirty: target.isDirty,
        isDeleted: target.isDeleted,
        isInEdit: false
      };
    };

    $scope.saveDocFile = function saveDocFile(target, form) {
      return form.$validate().then(function () {
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
        target.file = target.prevValues.file;
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
        docFileId: undefined,
        docId: id,
        docFileKindId: undefined,
        docFileKind: {},
        docFileTypeId: undefined,
        docFileType: {},
        file: undefined,
        isNew: true,
        isDirty: false,
        isDeleted: false,
        isInEdit: true
      };

      $scope.model.docFiles = $scope.model.docFiles || [];

      return Nomenclature.query({ alias: 'docFileKind' }).$promise
        .then(function (result) {
          var docFileKind = _(result).filter({ alias: 'PublicAttachedFile' }).first();

          docFile.docFileKindId = docFileKind.nomValueId;
          docFile.docFileKind = docFileKind;
          $scope.model.docFiles.push(docFile);
        });
    };
  }

  DocFilesEditCtrl.$inject = [
    '$scope',
    'Nomenclature'
  ];

  angular.module('ems').controller('DocFilesEditCtrl', DocFilesEditCtrl);
}(angular, _));
