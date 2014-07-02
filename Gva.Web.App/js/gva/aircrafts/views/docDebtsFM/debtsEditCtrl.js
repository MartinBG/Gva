/*global angular,_*/
(function (angular) {
  'use strict';

  function DocDebtsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebtsFM,
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
          return AircraftDocumentDebtsFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debtsFM.search');
            });
        }
      });
    };

    $scope.deleteDocumentDebt = function () {
      return AircraftDocumentDebtsFM.remove({
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
    'AircraftDocumentDebtsFM',
    'aircraftDocumentDebt'
  ];

  DocDebtsFMEditCtrl.$resolve = {
    aircraftDocumentDebt: [
      '$stateParams',
      'AircraftDocumentDebtsFM',
      function ($stateParams, AircraftDocumentDebtsFM) {
        return AircraftDocumentDebtsFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsFMEditCtrl', DocDebtsFMEditCtrl);
}(angular));