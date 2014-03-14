/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtFM,
    debts
  ) {
    $scope.debts = debts;


    $scope.editDocumentDebt = function (debt) {
      return $state.go('root.aircrafts.view.debtsFM.edit', {
        id: $stateParams.id,
        ind: debt.partIndex
      });
    };

    $scope.deleteDocumentDebt = function (debt) {
      return AircraftDocumentDebtFM.remove({ id: $stateParams.id, ind: debt.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftDocumentDebtFM',
    'debts'
  ];

  DocDebtsFMSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebtFM',
      function ($stateParams, AircraftDocumentDebtFM) {
        return AircraftDocumentDebtFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMSearchCtrl', DocDebtsFMSearchCtrl);
}(angular));
