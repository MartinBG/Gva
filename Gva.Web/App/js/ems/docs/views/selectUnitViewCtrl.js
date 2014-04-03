/*global angular, _*/
(function (angular) {
  'use strict';

  function SelectUnitViewCtrl(
    $state,
    $stateParams,
    $scope,
    units,
    selectedUnits
  ) {
    if (!selectedUnits.onUnitSelect) {
      return $state.go('^');
    }

    $scope.units = _.map(units, function (unit) {
      var su = _(selectedUnits.units).filter({ nomValueId: unit.nomValueId }).first();

      if (!su) {
        return unit;
      }
    });

    $scope.units = _.filter($scope.units, function (unit) {
      return !!unit;
    });

    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function Search() {
      return $state.go($state.current, {
        name: $scope.filters.name
      });
    };

    $scope.selectUnit = function SelectUnit(unit) {
      selectedUnits.onUnitSelect(unit);
      return $state.go('^');
    };

    $scope.cancel = function Cancel() {
      return $state.go('^');
    };
  }

  SelectUnitViewCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'units',
    'selectedUnits'
  ];

  SelectUnitViewCtrl.$resolve = {
    units: [
      '$stateParams',
      'Nomenclature',
      function ResolveUnits($stateParams, Nomenclature) {
        var params = _.assign({ alias: 'units' }, { name: $stateParams.name });

        return Nomenclature.query(params).$promise;
      }
    ]
  };

  angular.module('ems').controller('SelectUnitViewCtrl', SelectUnitViewCtrl);
}(angular, _));
