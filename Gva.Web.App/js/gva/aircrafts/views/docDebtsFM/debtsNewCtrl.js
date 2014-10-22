/*global angular*/
(function (angular) {
  'use strict';

  function DocDebtsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtsFM,
    aircraftDocumentDebt
  ) {
    $scope.debt = aircraftDocumentDebt;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentDebtForm.$validate()
         .then(function () {
            if ($scope.newDocumentDebtForm.$valid) {
              return AircraftDocumentDebtsFM
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
    'AircraftDocumentDebtsFM',
    'aircraftDocumentDebt'
  ];

  DocDebtsFMNewCtrl.$resolve = {
    aircraftDocumentDebt: [
      '$stateParams',
      'AircraftDocumentDebtsFM',
      function ($stateParams, AircraftDocumentDebtsFM) {
        return AircraftDocumentDebtsFM.newDocumentDebtFM({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMNewCtrl', DocDebtsFMNewCtrl);
}(angular));
