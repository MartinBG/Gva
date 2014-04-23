/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOpersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperational,
    equipmentCertOper
  ) {
    var originalCert = _.cloneDeep(equipmentCertOper);

    $scope.equipmentCertOper = equipmentCertOper;
    $scope.editMode = null;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editDocumentOperForm.$validate()
        .then(function () {
          if ($scope.editDocumentOperForm.$valid) {
            return EquipmentCertOperational
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
      $scope.equipmentCertOper.part = _.cloneDeep(originalCert.part);
    };
    
    $scope.deleteOper = function () {
      return EquipmentCertOperational.remove({
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
    'EquipmentCertOperational',
    'equipmentCertOper'
  ];

  EquipmentOpersEditCtrl.$resolve = {
    equipmentCertOper: [
      '$stateParams',
      'EquipmentCertOperational',
      function ($stateParams, EquipmentCertOperational) {
        return EquipmentCertOperational.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOpersEditCtrl', EquipmentOpersEditCtrl);
}(angular, _));