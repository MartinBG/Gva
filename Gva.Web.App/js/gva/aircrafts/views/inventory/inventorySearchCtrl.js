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

    $scope.getState = function (item) {
      var params,
          stateName;

      if (item.setPartAlias === 'aircraftApplication') {
        params = { 
          ind: item.partIndex,
          id: item.applicationId,
          set: 'aircraft',
          setPartPath: 'aircraftApplication',
          lotId: $stateParams.id
        };
      } else {
        params = { ind: item.partIndex };
      }
      switch(item.setPartAlias) { 
        case 'aircraftOther':
          stateName = 'root.aircrafts.view.others.edit';
          break;
        case 'aircraftOwner':
          stateName = 'root.aircrafts.view.owners.edit';
          break;
        case 'aircraftOccurrence':
          stateName = 'root.aircrafts.view.occurrences.edit';
          break;
        case 'aircraftDebtFM':
          stateName = 'root.aircrafts.view.debtsFM.edit';
          break;
        case 'aircraftApplication':
          stateName = 'root.applications.edit.data';
          break;
      }

      return {
        state: stateName,
        params: params
      };
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
