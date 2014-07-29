/*global angular*/
(function (angular) {
  'use strict';

  function CorrsDataCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.editCorrContact = function editCorrContact(target) {
      target.isInEdit = true;
      target.prevValues = {
        correspondentContactId: target.correspondentContactId,
        correspondentId: target.correspondentId,
        name: target.name,
        uin: target.uin,
        note: target.note,
        isActive: target.isActive,
        isNew: target.isNew,
        isDirty: target.isDirty,
        isDeleted: target.isDeleted,
        isInEdit: false
      };
    };

    $scope.saveCorrContact = function saveCorrContact(target) {
      target.isDirty = true;
      target.isInEdit = false;
      target.prevValues = undefined;
    };

    $scope.cancelCorrContact = function cancelCorrContact(target) {
      if (target.prevValues) {
        target.correspondentContactId = target.prevValues.correspondentContactId;
        target.correspondentId = target.prevValues.correspondentId;
        target.name = target.prevValues.name;
        target.uin = target.prevValues.uin;
        target.note = target.prevValues.note;
        target.isActive = target.prevValues.isActive;
        target.isNew = target.prevValues.isNew;
        target.isDirty = target.prevValues.isDirty;
        target.isDeleted = target.prevValues.isDeleted;
        target.isInEdit = target.prevValues.isInEdit;

        target.prevValues = undefined;
      } else {
        target.isInEdit = false;
      }
    };

    $scope.removeCorrContact = function removeCorrContact(target) {
      target.isDeleted = true;
    };

    $scope.addCorrContact = function addCorrContact(id) {
      var correspondentContact = {
        correspondentContactId: null,
        correspondentId: id,
        name: undefined,
        uin: undefined,
        note: undefined,
        isActive: true,
        isNew: true,
        isDirty: false,
        isDeleted: false,
        isInEdit: true
      };

      $scope.model.correspondentContacts = $scope.model.correspondentContacts || [];
      $scope.model.correspondentContacts.push(correspondentContact);
    };

  }

  CorrsDataCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('ems').controller('CorrsDataCtrl', CorrsDataCtrl);
}(angular));
