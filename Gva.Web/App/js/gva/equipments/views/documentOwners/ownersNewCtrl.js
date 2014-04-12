/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOwner,
    equipmentDocumentOwner
  ) {
    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return EquipmentDocumentOwner
              .save({ id: $stateParams.id }, $scope.equipmentDocumentOwner).$promise
              .then(function () {
                return $state.go('root.equipments.view.owners.search');
              });
          }
        });
    };

    $scope.equipmentDocumentOwner = equipmentDocumentOwner;

    $scope.cancel = function () {
      return $state.go('root.equipments.view.owners.search');
    };
  }

  EquipmentOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOwner',
    'equipmentDocumentOwner'
  ];

  EquipmentOwnersNewCtrl.$resolve = {
    equipmentDocumentOwner: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersNewCtrl', EquipmentOwnersNewCtrl);
}(angular));