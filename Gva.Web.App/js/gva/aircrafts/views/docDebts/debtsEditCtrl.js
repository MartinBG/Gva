/*global angular,_*/
(function (angular) {
  'use strict';

  function DocDebtsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebts,
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
          return AircraftDocumentDebts
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debts.search');
            });
        }
      });
    };

    $scope.deleteDocumentDebt = function () {
      return AircraftDocumentDebts.remove({
        id: $stateParams.id,
        ind: aircraftDocumentDebt.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.debts.search');
        });
    };
  }

  DocDebtsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentDebts',
    'aircraftDocumentDebt'
  ];

  DocDebtsEditCtrl.$resolve = {
    aircraftDocumentDebt: [
      '$stateParams',
      'AircraftDocumentDebts',
      function ($stateParams, AircraftDocumentDebts) {
        return AircraftDocumentDebts.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocDebtsEditCtrl', DocDebtsEditCtrl);
}(angular));