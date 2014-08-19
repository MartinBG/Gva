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
