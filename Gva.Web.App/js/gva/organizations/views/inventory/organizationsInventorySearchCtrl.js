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

    $scope.getState = function (item) {
      var params,
          stateName;

      if (item.setPartAlias === 'organizationApplication') {
        params = { 
          ind: item.partIndex,
          id: item.applicationId,
          set: 'organization',
          lotId: $stateParams.id
        };
      } else {
        params = { ind: item.partIndex };
      }
      switch(item.setPartAlias) { 
        case 'organizationOther':
          stateName = 'root.organizations.view.documentOthers.edit';
          break;
        case 'organizationApplication':
          stateName = 'root.applications.edit.data';
          break;
      }

      return {
        state: stateName,
        params: params
      };
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
