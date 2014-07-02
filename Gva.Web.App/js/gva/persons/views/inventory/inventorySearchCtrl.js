/*global angular, _*/
(function (angular, _) {
  'use strict';

  function InventorySearchCtrl(
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

      if (item.setPartAlias === 'personEducation') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (item.setPartAlias === 'personDocumentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (item.setPartAlias === 'personTraining') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (item.setPartAlias === 'personMedical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (item.setPartAlias === 'personCheck') {
        state = 'root.persons.view.checks.edit';
      }
      else if (item.setPartAlias === 'personOther') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (item.setPartAlias === 'personApplication') {
        state = 'root.persons.view.documentApplications.edit';
      }
      else if (item.setPartAlias === 'personEmployment') {
        state = 'root.persons.view.employments.edit';
      }

      return $state.go(state, { ind: item.partIndex });
    };
  }

  InventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'inventory'
  ];

  InventorySearchCtrl.$resolve = {
    inventory: [
      '$stateParams',
      'PersonsInventory',
      function ($stateParams, PersonsInventory) {
        return PersonsInventory.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('InventorySearchCtrl', InventorySearchCtrl);
}(angular, _));
