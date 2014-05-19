/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOwner,
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

    $scope.deleteDocumentOwner = function (documentOwner) {
      return EquipmentDocumentOwner.remove({ id: $stateParams.id, ind: documentOwner.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'EquipmentDocumentOwner',
    'documentOwners'
  ];

  EquipmentOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'EquipmentDocumentOwner',
      function ($stateParams, EquipmentDocumentOwner) {
        return EquipmentDocumentOwner.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOwnersSearchCtrl', EquipmentOwnersSearchCtrl);
}(angular));
