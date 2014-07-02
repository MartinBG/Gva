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
