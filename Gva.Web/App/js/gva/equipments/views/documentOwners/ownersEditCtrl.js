/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOwner,
    equipmentDocumentOwner
  ) {
    var originalDoc = _.cloneDeep(equipmentDocumentOwner);

    $scope.equipmentDocumentOwner = equipmentDocumentOwner;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return EquipmentDocumentOwner
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.owners.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentDocumentOwner.part = _.cloneDeep(originalDoc.part);
    };
    
    $scope.deleteInspection = function () {
      return EquipmentDocumentOwner.remove({
        id: $stateParams.id,
        ind: equipmentDocumentOwner.partIndex
      }).$promise.then(function () {
        return $state.go('root.equipments.view.owners.search');
      });
    };
  }

  EquipmentOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOwner',
    'equipmentDocumentOwner'
  ];

  EquipmentOwnersEditCtrl.$resolve = {
    equipmentDocumentOwner: [
      '$stateParams',
      'EquipmentDocumentOwner',
      function ($stateParams, EquipmentDocumentOwner) {
        return EquipmentDocumentOwner.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersEditCtrl', EquipmentOwnersEditCtrl);
}(angular, _));