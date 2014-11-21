/*global angular*/
(function (angular) {
  'use strict';

  function NomenclaturesCtrl(
    $scope,
    $state,
    $stateParams,
    NomenclaturesRsrc,
    nomenclatures
  ) {
    $scope.nomenclatures = nomenclatures;

    $scope.editNomenclature = function (item) {
      return $state.go('root.nomenclatures.edit', { id: item.nomId });
    };

    $scope.viewNomenclatureValues = function (item) {
      return $state.go('root.nomenclatures.values', { nomId: item.nomId });
    };
  }

  NomenclaturesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'NomenclaturesRsrc',
    'nomenclatures'
  ];

  NomenclaturesCtrl.$resolve = {
    nomenclatures: [
      'NomenclaturesRsrc',
      function resolveNomenclatures(NomenclaturesRsrc) {
        return NomenclaturesRsrc.query().$promise;
      }
    ]
  };

  angular.module('common').controller('NomenclaturesCtrl', NomenclaturesCtrl);
}(angular));
