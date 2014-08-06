/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebts,
    aircraftDocumentDebt
  ) {
    $scope.isEdit = false;
    $scope.debt = aircraftDocumentDebt;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentDebtForm.$validate()
         .then(function () {
            if ($scope.newDocumentDebtForm.$valid) {
              return AircraftDocumentDebts
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
    'AircraftDocumentDebts',
    'aircraftDocumentDebt'
  ];
  DocDebtsNewCtrl.$resolve = {
    aircraftDocumentDebt: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('DocDebtsNewCtrl', DocDebtsNewCtrl);
}(angular));
