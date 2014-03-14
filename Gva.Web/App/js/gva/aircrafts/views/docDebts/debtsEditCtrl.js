/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebt,
    aircraftDocumentDebt
  ) {
    $scope.isEdit = true;

    $scope.debt = aircraftDocumentDebt;

    $scope.save = function () {
      $scope.aircraftDocumentDebtForm.$validate()
      .then(function () {
        if ($scope.aircraftDocumentDebtForm.$valid) {
          return AircraftDocumentDebt
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debts.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.debts.search');
    };
  }

  DocDebtsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebt',
    'aircraftDocumentDebt'
  ];

  DocDebtsEditCtrl.$resolve = {
    aircraftDocumentDebt: [
      '$stateParams',
      'AircraftDocumentDebt',
      function ($stateParams, AircraftDocumentDebt) {
        return AircraftDocumentDebt.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsEditCtrl', DocDebtsEditCtrl);
}(angular));