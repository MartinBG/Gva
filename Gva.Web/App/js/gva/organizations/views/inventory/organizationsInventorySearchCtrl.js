/*global angular*/
(function (angular) {
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

    $scope.edit = function (item) {
      var state;

      if (item.documentType === 'other') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (item.documentType === 'application') {
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
}(angular));
