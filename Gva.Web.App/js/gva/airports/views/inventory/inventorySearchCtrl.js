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
    
    $scope.getState = function (setPartAlias) {
      switch(setPartAlias) { 
      case 'airportOther':
        return 'root.airports.view.others.edit';
      case 'airportOwner':
        return 'root.airports.view.owners.edit';
      case 'airportApplication':
        return 'root.airports.view.applications.edit';
      }
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
