/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsInventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    OrganizationInventory,
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

      if (item.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (item.setPartAlias === 'organizationApplication') {
        state = 'root.organizations.view.documentApplications.edit';
      }

      return $state.go(state, { ind: item.partIndex });
    };
  }

  OrganizationsInventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'OrganizationInventory',
    'inventory'
  ];

  OrganizationsInventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'OrganizationInventory',
      function ($stateParams, OrganizationInventory) {
        return OrganizationInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInventorySearchCtrl', OrganizationsInventorySearchCtrl);
}(angular, _));
