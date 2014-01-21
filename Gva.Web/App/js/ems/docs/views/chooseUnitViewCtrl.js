/*global angular, _*/
(function (angular) {
  'use strict';

  function ChooseUnitViewCtrl(
    $state,
    $stateParams,
    $scope,
    Unit
  ) {
    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      $state.go('docs/edit/chooseUnit', {
        name: $scope.filters.name
      });
    };

    Unit.query($stateParams).$promise.then(function (units) {
      $scope.units = units.map(function (unit) {
        return unit;
      });
    });

    $scope.selectUnit = function (unit) {
      $scope.onUnitSelected(unit);
      //todo goto previous state
      $state.go('docs/edit/addressing');
    };

    $scope.goBack = function () {
      //todo goto previous state
      $state.go('docs/edit/addressing');
    };

  }

  ChooseUnitViewCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'Unit'
  ];

  angular.module('ems').controller('ChooseUnitViewCtrl', ChooseUnitViewCtrl);
}(angular, _));
