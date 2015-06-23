/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function InventorySearchCtrl(
    $scope,
    $state,
    $stateParams,
    scModal,
    inventory
  ) {
    $scope.inventory = inventory;
    $scope.indexed = _.filter(inventory, function(item) {
       return item.bookPageNumber;
    });
    $scope.notIndexed = _.filter(inventory, function(item) {
       return !item.bookPageNumber;
    });

    $scope.changeCaseType = function (item) {
      var params = {
        setPartAlias: item.setPartAlias,
        partIndex: item.partIndex,
        parentPartIndex: item.parentPartIndex,
        applicationId: item.applicationId,
        lotId: $stateParams.id
      };

      var modalInstance = scModal.open('changeCaseType', params);
      modalInstance.result.then(function (caseType) {
        if (caseType) {
          $state.go($state.current, $stateParams, {reload: true});
        }
      });

      return modalInstance.opened;
    };

    $scope.getState = function (item) {
      var params,
          stateName;

      if(item.setPartAlias === 'personLicence') {
        params = { ind: item.parentPartIndex, index: item.partIndex };
      } else if (item.setPartAlias === 'personApplication') {
        params = { id: item.applicationId };
      } else {
        params = { ind: item.partIndex };
      }
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
          stateName = 'root.applications.edit.data';
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
      }

      return {
        state: stateName,
        params: params
      };
    };

    $scope.isInvalidDocument = function(item){
      return item.valid === false;
    };

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.toDate).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.toDate);
    };

  }

  InventorySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scModal',
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
}(angular, moment, _));
