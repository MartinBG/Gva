﻿/*global angular,_*/
(function (angular,_) {
  'use strict';
  function EquipmentCertOperationalCtrl($scope, $state, namedModal) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = namedModal.open('chooseEquipmentsDocs', {
        includedDocs: _.pluck($scope.model.includedDocuments, 'partIndex')
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.includedDocuments = $scope.model.includedDocuments.concat(selectedDocs);
      });

      return modalInstance.opened;
    };

    $scope.viewDocument = function (document) {
      var state;

      if (document.setPartAlias === 'equipmentOther') {
        state = 'root.equipments.view.others.edit';
      }
      else if (document.setPartAlias === 'equipmentOwner') {
        state = 'root.equipments.view.owners.edit';
      }
      else if (document.setPartAlias === 'equipmentApplication') {
        state = 'root.equipments.view.applications.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  EquipmentCertOperationalCtrl.$inject = ['$scope', '$state', 'namedModal'];

  angular.module('gva').controller('EquipmentCertOperationalCtrl', EquipmentCertOperationalCtrl);
}(angular,_));
