﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOpersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperationals,
    equipmentCertOper,
    scMessage
  ) {
    var originalCert = _.cloneDeep(equipmentCertOper);

    $scope.equipmentCertOper = equipmentCertOper;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

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
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return EquipmentCertOperationals.remove({
            id: $stateParams.id,
            ind: $stateParams.ind
          }).$promise.then(function () {
            return $state.go('root.equipments.view.opers.search');
          });
        }
      });
    };
  }

  EquipmentOpersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentCertOperationals',
    'equipmentCertOper',
    'scMessage'
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
