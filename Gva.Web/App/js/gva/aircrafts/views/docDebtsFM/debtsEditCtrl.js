/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtFM,
    aircraftDocumentDebt
  ) {
    $scope.isEdit = true;

    $scope.debt = aircraftDocumentDebt;

    $scope.save = function () {
      return $scope.aircraftDocumentDebtForm.$validate()
      .then(function () {
        if ($scope.aircraftDocumentDebtForm.$valid) {
          return AircraftDocumentDebtFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debtsFM.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.debtsFM.search');
    };
  }

  DocDebtsFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebtFM',
    'aircraftDocumentDebt'
  ];

  DocDebtsFMEditCtrl.$resolve = {
    aircraftDocumentDebt: [
      '$stateParams',
      'AircraftDocumentDebtFM',
      function ($stateParams, AircraftDocumentDebtFM) {
        return AircraftDocumentDebtFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMEditCtrl', DocDebtsFMEditCtrl);
}(angular));