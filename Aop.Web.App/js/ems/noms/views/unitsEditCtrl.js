/*global angular*/
(function (angular) {
  'use strict';

  function UnitsEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Units,
    unit,
    scMessage
  ) {
    $scope.unit = unit;

    $scope.save = function save() {
      return $scope.unitForm.$validate().then(function () {
        if ($scope.unitForm.$valid) {
          return Units.save({ id: $stateParams.id }, $scope.unit).$promise.then(function () {
            return $state.go('root.noms.units.search');
          });
        }
      });
    };

    $scope.deleteUnit = function () {
      return scMessage('noms.units.edit.confirmDelete').then(function (result) {
        if (result === 'OK') {
          return Units.remove({ id: $stateParams.id }).$promise.then(function () {
            return $state.go('root.noms.units.search');
          });
        }
      });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.noms.units.search');
    };
  }

  UnitsEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Units',
    'unit',
    'scMessage'
  ];

  UnitsEditCtrl.$resolve = {
    unit: [
      '$stateParams',
      'Units',
      function resolveUnit($stateParams, Units) {
        return Units.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('UnitsEditCtrl', UnitsEditCtrl);
}(angular));
