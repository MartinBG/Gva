/*global angular,_*/
(function (angular,_) {
  'use strict';
  function EquipmentCertOperationalCtrl(
    $scope,
    $state,
    scModal,
    scFormParams
  ) {
    $scope.lotId = scFormParams.lotId;

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = scModal.open('chooseEquipmentsDocs', {
        includedDocs: _.pluck($scope.model.includedDocuments, 'partIndex'),
        lotId: $scope.lotId
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.includedDocuments = $scope.model.includedDocuments.concat(selectedDocs);
      });

      return modalInstance.opened;
    };

    $scope.viewDocument = function (document) {
      var state;
      var params = {};
      if (document.setPartAlias === 'equipmentOther') {
        state = 'root.equipments.view.others.edit';
        params = { ind: document.partIndex };
      }
      else if (document.setPartAlias === 'equipmentOwner') {
        state = 'root.equipments.view.owners.edit';
        params = { ind: document.partIndex };
      }
      else if (document.setPartAlias === 'equipmentApplication') {
        state = 'root.applications.edit.data';
        params = { 
          ind: document.partIndex,
          id: document.applicationId,
          set: 'equipment',
          lotId: $scope.lotId
        };
      }

      return $state.go(state, params);
    };
  }

  EquipmentCertOperationalCtrl.$inject = [
    '$scope',
    '$state',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva').controller('EquipmentCertOperationalCtrl', EquipmentCertOperationalCtrl);
}(angular,_));
