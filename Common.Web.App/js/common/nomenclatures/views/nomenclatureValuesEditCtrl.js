/*global angular*/
(function (angular) {
  'use strict';

  function NomenclaturevaluesEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    NomenclatureValuesRsrc,
    nomenclatureValue,
    nomType,
    caseTypeOptions,
    scMessage
  ) {
    $scope.nomenclatureValue = nomenclatureValue;
    $scope.alias = $stateParams.alias;
    $scope.caseTypeOptions = caseTypeOptions;
    $scope.nomType = nomType;

    $scope.save = function save() {
      return $scope.nomenclatureForm.$validate().then(function () {
        if ($scope.nomenclatureForm.$valid) {
          return NomenclatureValuesRsrc.save({
            nomId: $stateParams.nomId,
            id: $stateParams.id
          }, $scope.nomenclatureValue)
            .$promise.then(function () {
              return $state.go('root.nomenclatures.search.values',
                { nomId: $stateParams.nomId, alias: $scope.alias },
                { reload: true });
            });
        }
      });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.nomenclatures.search.values', {
        nomId: $stateParams.nomId,
        alias: $scope.alias
      });
    };

    $scope.deleteNomenclatureValue = function () {
      return scMessage('Моля, потвърдете изтриването на тази номенклатура ?')
        .then(function (result) {
          if (result === 'OK') {
            return NomenclatureValuesRsrc.remove({
              nomId: $stateParams.nomId,
              id: $stateParams.id
            }).$promise.then(function () {
              return $state.go('root.nomenclatures.search.values',
                { nomId: $stateParams.nomId, alias: $scope.alias },
                { reload: true });
            });
          }
        });
    };
  }

  NomenclaturevaluesEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'NomenclatureValuesRsrc',
    'nomenclatureValue',
    'nomType',
    'caseTypeOptions',
    'scMessage'
  ];

  NomenclaturevaluesEditCtrl.$resolve = {
    nomenclatureValue: [
      '$stateParams',
      'NomenclatureValuesRsrc',
      function resolveUnit($stateParams, NomenclatureValuesRsrc) {
        if ($stateParams.id) {
          return NomenclatureValuesRsrc.get({
            nomId: $stateParams.nomId,
            id: $stateParams.id
          }).$promise;
        } else {
          return {
            nomId: $stateParams.nomId
          };
        }
      }
    ],
    caseTypeOptions: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.alias === 'applicationTypes' ||
          $stateParams.alias === 'documentTypes' ||
          $stateParams.alias === 'documentRoles') {
          return Nomenclatures.query({
            alias: 'caseTypes'
          }).$promise;
        } else {
          return null;
        }
      }
    ],
    nomType: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        return Nomenclatures.get({
          alias: 'nomList',
          id: $stateParams.nomId
        }).$promise;
      }
    ]
  };

  angular.module('common').controller('NomenclaturevaluesEditCtrl', NomenclaturevaluesEditCtrl);
}(angular));
