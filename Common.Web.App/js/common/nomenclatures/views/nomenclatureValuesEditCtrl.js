/*global angular, _*/
(function (angular, _) {
  'use strict';

  function NomenclaturevaluesEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    NomenclatureValuesRsrc,
    nomenclatureValue,
    Nomenclatures,
    nomType,
    caseTypeOptions,
    templateOptions,
    qualificationOptions,
    scMessage,
    scModal
  ) {
    $scope.nomenclatureValue = nomenclatureValue;
    $scope.alias = $stateParams.alias;
    $scope.caseTypeOptions = caseTypeOptions;
    $scope.nomType = nomType;
    $scope.templateOptions = templateOptions;
    $scope.qualificationOptions = qualificationOptions;

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

    $scope.chooseDataGenerator = function () {
      var params = {
        template: _.where($scope.templateOptions, {
            code: $scope.nomenclatureValue.textContent.templateName
          })[0],
        dataGenerator: $scope.dataGenerator.code
      };

      var modalInstance = scModal.open('chooseDataGenerator', params);

      modalInstance.result.then(function (dataGenerator) {
        $scope.dataGenerator = dataGenerator;
      });

      return modalInstance.opened;
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

    if ($stateParams.alias === 'licenceTypes') {
      $scope.$watch('nomenclatureValue.textContent.templateName', function () {
        if ($scope.nomenclatureValue.textContent &&
          $scope.nomenclatureValue.textContent.templateName) {
          Nomenclatures.get({
            alias: 'dataGenerators',
            templateName: $scope.nomenclatureValue.textContent.templateName
         })
          .$promise
          .then(function (nom) {
            $scope.dataGenerator = nom;
          });
        }
      });
    }
  }

  NomenclaturevaluesEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'NomenclatureValuesRsrc',
    'nomenclatureValue',
    'Nomenclatures',
    'nomType',
    'caseTypeOptions',
    'templateOptions',
    'qualificationOptions',
    'scMessage',
    'scModal'
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
          $stateParams.alias === 'documentRoles' ||
          $stateParams.alias === 'licenceTypes') {
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
    ],
    templateOptions: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.alias === 'licenceTypes') {
          return Nomenclatures.query({
            alias: 'templates'
          }).$promise;
        } else {
          return null;
        }
      }
    ],
    qualificationOptions: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.alias === 'licenceTypes') {
          return Nomenclatures.query({
            alias: 'appExSystQualifications'
          }).$promise;
        } else {
          return null;
        }
      }
    ]
  };

  angular.module('common').controller('NomenclaturevaluesEditCtrl', NomenclaturevaluesEditCtrl);
}(angular, _));
