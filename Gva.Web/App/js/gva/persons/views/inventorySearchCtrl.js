/*global angular, _*/
(function (angular, _) {
  'use strict';

  function InventorySearchCtrl($scope, $state, $stateParams, $filter, PersonInventory) {
    PersonInventory.query($stateParams).$promise.then(function (inventory) {
      $scope.inventory = _.map(inventory, function (element) {
        element.date = $filter('date')(element.date, 'dd.MM.yyyy');
        element.fromDate = $filter('date')(element.fromDate, 'dd.MM.yyyy');
        element.toDate = $filter('date')(element.toDate, 'dd.MM.yyyy');
        element.creationDate = $filter('date')(element.creationDate, 'dd.MM.yyyy');
        element.editedDate = $filter('date')(element.editedDate, 'dd.MM.yyyy');

        return element;
      });
    });

    $scope.edit = function (item) {
      var state;

      if (item.documentType === 'education') {
        state = 'persons.documentEducations.edit';
      }
      else if (item.documentType === 'documentId') {
        state = 'persons.documentIds.edit';
      }
      else if (item.documentType === 'training') {
        state = 'persons.documentTrainings.edit';
      }
      else if (item.documentType === 'medical') {
        state = 'persons.medicals.edit';
      }
      else if (item.documentType === 'check') {
        state = 'persons.checks.edit';
      }

      $state.go(state, { ind: item.partIndex });
    };
  }

  InventorySearchCtrl.$inject = ['$scope', '$state', '$stateParams', '$filter', 'PersonInventory'];

  angular.module('gva').controller('InventorySearchCtrl', InventorySearchCtrl);
}(angular, _));
