/*global angular, _*/
(function (angular, _) {
  'use strict';

  function InventorySearchCtrl(
    $scope,
    inventory
  ) {
    $scope.inventory = inventory;
    $scope.indexed = _.filter(inventory, function(item) {
       return item.bookPageNumber;
    });
    $scope.notIndexed = _.filter(inventory, function(item) {
       return !item.bookPageNumber;
    });

    $scope.getState = function (item) {
      var params = item.setPartAlias === 'personLicence' ?
        { ind: item.parentPartIndex, index: item.partIndex } :
        { ind: item.partIndex },
          stateName;

      switch (item.setPartAlias) {
        case 'personEducation':
          stateName = 'root.persons.view.documentEducations.edit';
          break;
        case 'personDocumentId':
          stateName = 'root.persons.view.documentIds.edit';
          break;
        case 'personTraining':
          stateName = 'root.persons.view.documentTrainings.edit';
          break;
        case 'personMedical':
          stateName = 'root.persons.view.medicals.edit';
          break;
        case 'personCheck':
          stateName = 'root.persons.view.checks.edit';
          break;
        case 'personOther':
          stateName = 'root.persons.view.documentOthers.edit';
          break;
        case 'personApplication':
          stateName = 'root.persons.view.documentApplications.edit';
          break;
        case 'personEmployment':
          stateName = 'root.persons.view.employments.edit';
          break;
        case 'personLicence':
          stateName = 'root.persons.view.licences.view.editions.edit';
          break;
        case 'personLangCert':
          stateName = 'root.persons.view.documentLangCerts.edit';
          break;
        case 'personExams':
          stateName = 'root.persons.view.exams.edit';
          break;
      }

      return {
        state: stateName,
        params: params
      };
    };
  }

  InventorySearchCtrl.$inject = [
    '$scope',
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
