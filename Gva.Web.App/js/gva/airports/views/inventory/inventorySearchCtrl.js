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
    
    $scope.getState = function (item) {
      var params,
          stateName;

      if (item.setPartAlias === 'airportApplication') {
        params = { 
          ind: item.partIndex,
          id: item.applicationId,
          set: 'airport',
          lotId: $stateParams.id
        };
      } else {
        params = { ind: item.partIndex };
      }
      switch(item.setPartAlias) { 
        case 'airportOther':
          stateName = 'root.airports.view.others.edit';
          break;
        case 'airportOwner':
          stateName = 'root.airports.view.owners.edit';
          break;
        case 'airportApplication':
          stateName = 'root.applications.edit.data';
          break;
      }

      return {
        state: stateName,
        params: params
      };
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
