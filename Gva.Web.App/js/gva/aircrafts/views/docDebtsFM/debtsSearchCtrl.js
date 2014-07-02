/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtsFM,
    debts
  ) {
    $scope.debts = debts;

    $scope.editDocumentDebt = function (debt) {
      return $state.go('root.aircrafts.view.debtsFM.edit', {
        id: $stateParams.id,
        ind: debt.partIndex
      });
    };

    $scope.newDocumentDebt = function () {
      return $state.go('root.aircrafts.view.debtsFM.new');
    };
  }

  DocDebtsFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebtsFM',
    'debts'
  ];

  DocDebtsFMSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebtsFM',
      function ($stateParams, AircraftDocumentDebtsFM) {
        return AircraftDocumentDebtsFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMSearchCtrl', DocDebtsFMSearchCtrl);
}(angular));
