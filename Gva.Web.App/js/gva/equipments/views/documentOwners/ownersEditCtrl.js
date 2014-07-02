/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOwners,
    equipmentDocumentOwner
  ) {
    var originalDoc = _.cloneDeep(equipmentDocumentOwner);

    $scope.equipmentDocumentOwner = equipmentDocumentOwner;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentDocumentOwner = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return EquipmentDocumentOwners
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.owners.search');
              });
          }
        });
    };

    $scope.deleteOwner = function () {
      return EquipmentDocumentOwners.remove({
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
    'EquipmentDocumentOwners',
    'equipmentDocumentOwner'
  ];

  EquipmentOwnersEditCtrl.$resolve = {
    equipmentDocumentOwner: [
      '$stateParams',
      'EquipmentDocumentOwners',
      function ($stateParams, EquipmentDocumentOwners) {
        return EquipmentDocumentOwners.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersEditCtrl', EquipmentOwnersEditCtrl);
}(angular, _));