/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsInventorySearchCtrl(
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
      case 'organizationOther':
        return 'root.organizations.view.documentOthers.edit';
      case 'organizationApplication':
        return 'root.organizations.view.documentApplications.edit';
      }
    };
  }

  OrganizationsInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'inventory'
  ];

  OrganizationsInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'OrganizationsInventory',
      function ($stateParams, OrganizationsInventory) {
        return OrganizationsInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInventorySearchCtrl', OrganizationsInventorySearchCtrl);
}(angular, _));
