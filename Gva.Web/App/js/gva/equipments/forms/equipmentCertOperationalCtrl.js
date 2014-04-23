/*global angular*/
(function (angular) {
  'use strict';
  function EquipmentCertOperationalCtrl($scope, $state) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      $state.go('.chooseDocuments', {}, {}, {
        selectedDocuments: $scope.model.includedDocuments
      });
    };

    // coming from a child state and carrying payload
    if ($state.previous && $state.previous.includes[$state.current.name] && $state.payload) {
      if ($state.payload.selectedDocuments) {
        [].push.apply($scope.model.includedDocuments, $state.payload.selectedDocuments);
      }
    }

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
      else if (document.setPartAlias === 'inspection') {
        state = 'root.equipments.view.inspections.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  EquipmentCertOperationalCtrl.$inject = ['$scope','$state'];

  angular.module('gva').controller('EquipmentCertOperationalCtrl', EquipmentCertOperationalCtrl);
}(angular));
