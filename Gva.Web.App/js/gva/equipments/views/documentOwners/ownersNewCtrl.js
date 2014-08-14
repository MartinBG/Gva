/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOwners,
    equipmentDocumentOwner
  ) {
    $scope.equipmentDocumentOwner = equipmentDocumentOwner;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return EquipmentDocumentOwners
              .save({ id: $stateParams.id }, $scope.equipmentDocumentOwner).$promise
              .then(function () {
                return $state.go('root.equipments.view.owners.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.owners.search');
    };
  }

  EquipmentOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOwners',
    'equipmentDocumentOwner'
  ];

  EquipmentOwnersNewCtrl.$resolve = {
    equipmentDocumentOwner: [
      '$stateParams',
      'EquipmentDocumentOwners',
      function ($stateParams, EquipmentDocumentOwners) {
        return EquipmentDocumentOwners.newOwner({
          id: $stateParams.id,
          appId: $stateParams.appId
        });
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersNewCtrl', EquipmentOwnersNewCtrl);
}(angular));
