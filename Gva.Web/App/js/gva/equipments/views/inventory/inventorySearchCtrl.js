﻿/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentInventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    EquipmentInventory,
    inventory
  ) {
    $scope.inventory = inventory;

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
    'EquipmentInventory',
    'inventory'
  ];

  EquipmentInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'EquipmentInventory',
      function ($stateParams, EquipmentInventory) {
        return EquipmentInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentInventorySearchCtrl', EquipmentInventorySearchCtrl);
}(angular));
