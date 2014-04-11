/*global angular,_*/
(function (angular) {
  'use strict';

  function DocDebtsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtFM,
    aircraftDocumentDebt
  ) {
    var originalDebt = _.cloneDeep(aircraftDocumentDebt);

    $scope.isEdit = true;
    $scope.debt = aircraftDocumentDebt;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.debt = _.cloneDeep(originalDebt);
    };

    $scope.save = function () {
      return $scope.editDocumentDebtForm.$validate()
      .then(function () {
        if ($scope.editDocumentDebtForm.$valid) {
          return AircraftDocumentDebtFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debtsFM.search');
            });
        }
      });
    };

    $scope.deleteDocumentDebt = function () {
      return AircraftDocumentDebtFM.remove({
        id: $stateParams.id,
        ind: aircraftDocumentDebt.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.debtsFM.search');
        });
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