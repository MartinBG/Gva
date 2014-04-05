﻿/*global angular*/
(function (angular) {
  'use strict';

  function AircraftInventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    AircraftInventory,
    inventory
  ) {
    $scope.inventory = inventory;

    $scope.edit = function (item) {
      var state;

      if (item.documentType === 'aircraftOther') {
        state = 'root.aircrafts.view.others.edit';
      }
      else if (item.documentType === 'aircraftOwner') {
        state = 'root.aircrafts.view.owners.edit';
      }
      else if (item.documentType === 'aircraftOccurrence') {
        state = 'root.aircrafts.view.occurrences.edit';
      }
      else if (item.documentType === 'aircraftDebtFM') {
        state = 'root.aircrafts.view.debtsFM.edit';
      }
      else if (item.documentType === 'aircraftApplication') {
        state = 'root.aircrafts.view.applications.edit';
      }

      return $state.go(state, { ind: item.partIndex });
    };
  }

  AircraftInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'AircraftInventory',
    'inventory'
  ];

  AircraftInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'AircraftInventory',
      function ($stateParams, AircraftInventory) {
        return AircraftInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftInventorySearchCtrl', AircraftInventorySearchCtrl);
}(angular));
