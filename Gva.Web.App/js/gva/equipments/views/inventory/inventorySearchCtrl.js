/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentInventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    inventory
  ) {
    $scope.inventory = inventory;
    $scope.indexed = _.filter(inventory, function (item) {
      return item.bookPageNumber;
    });
    $scope.notIndexed = _.filter(inventory, function (item) {
      return !item.bookPageNumber;
    });

    $scope.getState = function (setPartAlias) {
      switch(setPartAlias) { 
      case 'equipmentOther':
        return 'root.equipments.view.others.edit';
      case 'equipmentOwner':
        return 'root.equipments.view.owners.edit';
      case 'equipmentApplication':
        return 'root.equipments.view.applications.edit';
      }
    };
  }

  EquipmentInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'inventory'
  ];

  EquipmentInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'EquipmentsInventory',
      function ($stateParams, EquipmentsInventory) {
        return EquipmentsInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentInventorySearchCtrl', EquipmentInventorySearchCtrl);
}(angular, _));
