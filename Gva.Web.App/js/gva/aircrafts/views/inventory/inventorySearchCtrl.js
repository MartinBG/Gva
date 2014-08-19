/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftInventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    inventory
  ) {
    $scope.inventory = inventory;
    $scope.indexed = _.filter(inventory, function(item) {
         return item.bookPageNumber;
      });
    $scope.notIndexed =  _.filter(inventory, function(item) {
         return !item.bookPageNumber;
      });

    $scope.getState = function (setPartAlias) {
      switch(setPartAlias) { 
      case 'aircraftOther':
        return 'root.aircrafts.view.others.edit';
      case 'aircraftOwner':
        return 'root.aircrafts.view.owners.edit';
      case 'aircraftOccurrence':
        return 'root.aircrafts.view.occurrences.edit';
      case 'aircraftDebtFM':
        return 'root.aircrafts.view.debtsFM.edit';
      case 'aircraftApplication':
        return 'root.aircrafts.view.applications.edit';
      }
    };
  }

  AircraftInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'inventory'
  ];

  AircraftInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'AircraftsInventory',
      function ($stateParams, AircraftsInventory) {
        return AircraftsInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftInventorySearchCtrl', AircraftInventorySearchCtrl);
}(angular, _));
