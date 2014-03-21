/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtFM,
    aircraftDocumentDebt
  ) {
    $scope.isEdit = false;

    $scope.debt = aircraftDocumentDebt;

    $scope.save = function () {
      return $scope.aircraftDocumentDebtForm.$validate()
         .then(function () {
            if ($scope.aircraftDocumentDebtForm.$valid) {
              return AircraftDocumentDebtFM
              .save({ id: $stateParams.id }, $scope.debt).$promise
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

  DocDebtsFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebtFM',
    'aircraftDocumentDebt'
  ];
  DocDebtsFMNewCtrl.$resolve = {
    aircraftDocumentDebt: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('DocDebtsFMNewCtrl', DocDebtsFMNewCtrl);
}(angular));
