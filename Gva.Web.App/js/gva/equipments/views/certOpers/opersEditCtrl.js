/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOpersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperationals,
    equipmentCertOper
  ) {
    var originalCert = _.cloneDeep(equipmentCertOper);

    $scope.equipmentCertOper = equipmentCertOper;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editDocumentOperForm.$validate()
        .then(function () {
          if ($scope.editDocumentOperForm.$valid) {
            return EquipmentCertOperationals
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentCertOper)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.opers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentCertOper = _.cloneDeep(originalCert);
    };

    $scope.deleteOper = function () {
      return EquipmentCertOperationals.remove({
        id: $stateParams.id,
        ind: equipmentCertOper.partIndex
      }).$promise.then(function () {
        return $state.go('root.equipments.view.opers.search');
      });
    };
  }

  EquipmentOpersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentCertOperationals',
    'equipmentCertOper'
  ];

  EquipmentOpersEditCtrl.$resolve = {
    equipmentCertOper: [
      '$stateParams',
      'EquipmentCertOperationals',
      function ($stateParams, EquipmentCertOperationals) {
        return EquipmentCertOperationals.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOpersEditCtrl', EquipmentOpersEditCtrl);
}(angular, _));