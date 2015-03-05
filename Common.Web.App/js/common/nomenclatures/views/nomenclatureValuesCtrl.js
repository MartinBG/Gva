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

    $scope.editNomenclatureValue = function (item) {
      return $state.go('root.nomenclatures.search.values.edit', {
        nomId: $stateParams.nomId,
        id: item.nomValueId,
        alias: $stateParams.alias
      });
    };

    $scope.newNomenclatureValue = function () {
      return $state.go('root.nomenclatures.search.values.edit', {
        nomId: $stateParams.nomId,
        id: undefined,
        alias: $stateParams.alias
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
