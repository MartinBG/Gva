/*global angular*/
(function (angular) {
  'use strict';

  function NomenclatureValuesCtrl(
    $scope,
    $state,
    $stateParams,
    NomenclatureValuesRsrc,
    nomenclatureValues
  ) {
    $scope.nomenclatureValues = nomenclatureValues;
    $scope.nomAlias = $stateParams.alias;

    $scope.editNomenclatureValue = function (item) {
      return $state.go('root.nomenclatures.search.values.edit', {
        nomId: $stateParams.nomId,
        id: item.nomValueId,
        alias: $scope.nomAlias
      });
    };

    $scope.newNomenclatureValue = function () {
      return $state.go('root.nomenclatures.search.values.edit', {
        nomId: $stateParams.nomId,
        alias: $scope.nomAlias
      });
    };
  }

  NomenclatureValuesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'NomenclatureValuesRsrc',
    'nomenclatureValues'
  ];

  NomenclatureValuesCtrl.$resolve = {
    nomenclatureValues: [
      '$stateParams',
      'NomenclatureValuesRsrc',
      function resolveNomenclatureValues($stateParams, NomenclatureValuesRsrc) {
        return NomenclatureValuesRsrc.query({ nomId: $stateParams.nomId }).$promise;
      }
    ]
  };

  angular.module('common').controller('NomenclatureValuesCtrl', NomenclatureValuesCtrl);
}(angular));
