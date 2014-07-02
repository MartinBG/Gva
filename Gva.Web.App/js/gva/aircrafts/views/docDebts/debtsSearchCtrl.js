/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebts,
    debts
  ) {
    $scope.debts = debts;

    $scope.editDocumentDebt = function (debt) {
      return $state.go('root.aircrafts.view.debts.edit', {
        id: $stateParams.id,
        ind: debt.partIndex
      });
    };

    $scope.newDocumentDebt = function () {
      return $state.go('root.aircrafts.view.debts.new');
    };
  }

  DocDebtsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebts',
    'debts'
  ];

  DocDebtsSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebts',
      function ($stateParams, AircraftDocumentDebts) {
        return AircraftDocumentDebts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsSearchCtrl', DocDebtsSearchCtrl);
}(angular));
