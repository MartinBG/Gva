/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebt,
    aircraftDocumentDebt
  ) {
    $scope.isEdit = false;

    $scope.debt = aircraftDocumentDebt;

    $scope.save = function () {
      $scope.aircraftDocumentDebtForm.$validate()
         .then(function () {
            if ($scope.aircraftDocumentDebtForm.$valid) {
              return AircraftDocumentDebt
              .save({ id: $stateParams.id }, $scope.debt).$promise
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

  DocDebtsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebt',
    'aircraftDocumentDebt'
  ];
  DocDebtsNewCtrl.$resolve = {
    aircraftDocumentDebt: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('DocDebtsNewCtrl', DocDebtsNewCtrl);
}(angular));
