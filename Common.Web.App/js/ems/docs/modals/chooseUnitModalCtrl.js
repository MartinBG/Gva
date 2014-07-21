/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseUnitModalCtrl(
    $modalInstance,
    $scope,
    Nomenclatures,
    units,
    selectedUnits
  ) {
    $scope.units = units;

    $scope.filters = {
      name: null
    };

    $scope.selectedUnits = _.map(selectedUnits, function (unit) {
      return unit.nomValueId;
    });

    $scope.search = function () {
      var params = {
        alias: 'employeeUnit',
        term: $scope.filters.name
      };

      return Nomenclatures.query(params).$promise.then(function (units) {
        $scope.units = units;
      });
    };

    $scope.selectUnit = function SelectUnit(unit) {
      return $modalInstance.close(unit);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseUnitModalCtrl.$inject = [
    '$modalInstance',
    '$scope',
    'Nomenclatures',
    'units',
    'selectedUnits'
  ];

  angular.module('ems').controller('ChooseUnitModalCtrl', ChooseUnitModalCtrl);
}(angular, _));
