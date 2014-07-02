/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportInventorySearchCtrl(
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

    $scope.edit = function (item) {
      var state;

      if (item.documentType === 'airportOther') {
        state = 'root.airports.view.others.edit';
      }
      else if (item.documentType === 'airportOwner') {
        state = 'root.airports.view.owners.edit';
      }
      else if (item.documentType === 'airportOccurrence') {
        state = 'root.airports.view.occurrences.edit';
      }
      else if (item.documentType === 'airportDebtFM') {
        state = 'root.airports.view.debtsFM.edit';
      }
      else if (item.documentType === 'airportApplication') {
        state = 'root.airports.view.applications.edit';
      }

      return $state.go(state, { ind: item.partIndex });
    };
  }

  AirportInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'inventory'
  ];

  AirportInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'AirportsInventory',
      function ($stateParams, AirportsInventory) {
        return AirportsInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportInventorySearchCtrl', AirportInventorySearchCtrl);
}(angular, _));
