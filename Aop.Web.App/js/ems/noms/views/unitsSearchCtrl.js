/*global angular, _*/
(function (angular, _) {
  'use strict';

  function UnitsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    units
  ) {
    $scope.units = units;

    $scope.filters = {
      name: null,
      unitTypeId: null
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newUnit = function newCorr() {
      return $state.go('root.noms.units.new');
    };

    $scope.search = function search() {
      return $state.go($state.current, {
        name: $scope.filters.name,
        unitTypeId: $scope.filters.unitTypeId
      }, { reload: true });
    };

    $scope.editUnit = function editCorr(unit) {
      return $state.go('root.noms.units.edit', { id: unit.unitId });
    };
  }

  UnitsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'units'
  ];

  UnitsSearchCtrl.$resolve = {
    units: [
      '$stateParams',
      'Units',
      function resolveUnits($stateParams, Units) {
        return Units.query($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('UnitsSearchCtrl', UnitsSearchCtrl);
}(angular, _));
