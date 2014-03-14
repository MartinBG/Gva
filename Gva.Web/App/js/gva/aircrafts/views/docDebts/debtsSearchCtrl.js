/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebt,
    debts
  ) {
    $scope.debts = debts;


    $scope.editDocumentDebt = function (debt) {
      return $state.go('root.aircrafts.view.debts.edit', {
        id: $stateParams.id,
        ind: debt.partIndex
      });
    };

    $scope.deleteDocumentDebt = function (debt) {
      return AircraftDocumentDebt.remove({ id: $stateParams.id, ind: debt.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftDocumentDebt',
    'debts'
  ];

  DocDebtsSearchCtrl.$resolve = {
    debts: [
      '$stateParams',
      'AircraftDocumentDebt',
      function ($stateParams, AircraftDocumentDebt) {
        return AircraftDocumentDebt.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsSearchCtrl', DocDebtsSearchCtrl);
}(angular));
