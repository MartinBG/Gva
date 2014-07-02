/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOwners
  ) {
    $scope.documentOwners = documentOwners;

    $scope.editDocumentOwner = function (documentOwner) {
      return $state.go('root.equipments.view.owners.edit',
        {
          id: $stateParams.id,
          ind: documentOwner.partIndex
        });
    };

    $scope.newDocumentOwner = function () {
      return $state.go('root.equipments.view.owners.new');
    };
  }

  EquipmentOwnersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOwners'
  ];

  EquipmentOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'EquipmentDocumentOwners',
      function ($stateParams, EquipmentDocumentOwners) {
        return EquipmentDocumentOwners.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersSearchCtrl', EquipmentOwnersSearchCtrl);
}(angular));
