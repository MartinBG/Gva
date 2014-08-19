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

    $scope.getState = function (setPartAlias) {
      switch(setPartAlias) { 
      case 'personEducation':
        return 'root.persons.view.documentEducations.edit';
      case 'personDocumentId':
        return 'root.persons.view.documentIds.edit';
      case 'personTraining':
        return 'root.persons.view.documentTrainings.edit';
      case  'personMedical':
        return 'root.persons.view.medicals.edit';
      case 'personCheck':
        return 'root.persons.view.checks.edit';
      case 'personOther':
        return 'root.persons.view.documentOthers.edit';
      case 'personApplication':
        return 'root.persons.view.documentApplications.edit';
      case 'personEmployment':
        return 'root.persons.view.employments.edit';
      }
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
