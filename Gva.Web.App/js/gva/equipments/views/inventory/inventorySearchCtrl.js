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

    $scope.edit = function (item) {
      var state;

      if (item.setPartAlias === 'equipmentOther') {
        state = 'root.equipments.view.others.edit';
      }
      else if (item.setPartAlias === 'equipmentOwner') {
        state = 'root.equipments.view.owners.edit';
      }
      else if (item.setPartAlias === 'inspection') {
        state = 'root.equipments.view.inspections.edit';
      }
      else if (item.setPartAlias === 'equipmentApplication') {
        state = 'root.equipments.view.applications.edit';
      }

      return $state.go(state, { ind: item.partIndex });
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
