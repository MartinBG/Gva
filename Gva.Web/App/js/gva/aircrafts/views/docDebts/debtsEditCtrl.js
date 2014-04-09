/*global angular,_*/
(function (angular) {
  'use strict';

  function DocDebtsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentDebt,
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
      $scope.debt.part = _.cloneDeep(originalDebt.part);
      $scope.$broadcast('cancel', originalDebt);
    };

    $scope.save = function () {
      return $scope.editDocumentDebtForm.$validate()
      .then(function () {
        if ($scope.editDocumentDebtForm.$valid) {
          return AircraftDocumentDebt
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.debt)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.debts.search');
            });
        }
      });
    };

    $scope.deleteDocumentDebt = function () {
      return AircraftDocumentDebt.remove({
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