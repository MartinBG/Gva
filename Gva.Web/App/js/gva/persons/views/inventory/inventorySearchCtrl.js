/*global angular*/
(function (angular) {
  'use strict';

  function InventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonInventory,
    inventory
  ) {
    $scope.inventory = inventory;

    $scope.edit = function (item) {
      var state;

      if (item.documentType === 'education') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (item.documentType === 'documentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (item.documentType === 'training') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (item.documentType === 'medical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (item.documentType === 'check') {
        state = 'root.persons.view.checks.edit';
      }
      else if (item.documentType === 'other') {
        state = 'root.persons.view.documentOthers.edit';
      }

      return $state.go(state, { ind: item.partIndex });
    };
  }

  InventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'PersonInventory',
    'inventory'
  ];

  InventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'PersonInventory',
      function ($stateParams, PersonInventory) {
        return PersonInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('InventorySearchCtrl', InventorySearchCtrl);
}(angular));
