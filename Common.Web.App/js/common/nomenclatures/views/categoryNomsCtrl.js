/*global angular*/
(function (angular) {
  'use strict';

  function CategoryNomsCtrl(
    $scope,
    $state,
    $stateParams,
    NomenclaturesRsrc,
    nomenclatures
  ) {
    $scope.nomenclatures = nomenclatures;

    $scope.viewNomenclatureValues = function (item) {
      return $state.go('root.nomenclatures.search.values', 
        { nomId: item.nomId, alias: item.alias });
    };
  }

  CategoryNomsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'NomenclaturesRsrc',
    'nomenclatures'
  ];

  CategoryNomsCtrl.$resolve = {
    nomenclatures: [
      '$stateParams',
      'NomenclaturesRsrc',
      function resolveNomenclatures($stateParams, NomenclaturesRsrc) {
        return NomenclaturesRsrc.query($stateParams).$promise;
      }
    ]
  };

  angular.module('common').controller('CategoryNomsCtrl', CategoryNomsCtrl);
}(angular));
