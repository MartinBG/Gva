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

    $scope.getState = function (item) {
      var params,
          stateName;

      if (item.setPartAlias === 'equipmentApplication') {
        params = { 
          ind: item.partIndex,
          id: item.applicationId,
          set: 'equipment',
          lotId: $stateParams.id
        };
      } else {
        params = { ind: item.partIndex };
      }
      switch(item.setPartAlias) { 
        case 'equipmentOther':
          stateName = 'root.equipments.view.others.edit';
          break;
        case 'equipmentOwner':
          stateName = 'root.equipments.view.owners.edit';
          break;
        case 'equipmentApplication':
          stateName = 'root.applications.edit.data';
          break;
      }

      return {
        state: stateName,
        params: params
      };
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
