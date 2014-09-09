/*global angular*/
(function (angular) {
  'use strict';

  function UnitsNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Units,
    unit
  ) {
    $scope.unit = unit;

    $scope.save = function save() {
      return $scope.unitForm.$validate().then(function () {
        if ($scope.unitForm.$valid) {
          return Units.save($scope.unit).$promise.then(function () {
            return $state.go('root.noms.units.search');
          });
        }
      });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.noms.units.search');
    };
  }

  UnitsNewCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Units',
    'unit'
  ];

  UnitsNewCtrl.$resolve = {
    unit: [
      '$stateParams',
      'Units',
      function resolveUnit($stateParams, Units) {
        return Units.getNew().$promise;
      }
    ]
  };

  angular.module('ems').controller('UnitsNewCtrl', UnitsNewCtrl);
}(angular));
