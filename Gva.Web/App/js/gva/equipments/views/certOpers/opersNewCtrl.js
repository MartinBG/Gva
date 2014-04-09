/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOpersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperational,
    equipmentCertOper
  ) {
    $scope.save = function () {
      return $scope.newCertOperForm.$validate()
        .then(function () {
          if ($scope.newCertOperForm.$valid) {
            return EquipmentCertOperational
              .save({ id: $stateParams.id }, $scope.equipmentCertOper).$promise
              .then(function () {
                return $state.go('root.equipments.view.opers.search');
              });
          }
        });
    };

    $scope.equipmentCertOper = equipmentCertOper;

    $scope.cancel = function () {
      return $state.go('root.equipments.view.opers.search');
    };
  }

  EquipmentOpersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentCertOperational',
    'equipmentCertOper'
  ];

  EquipmentOpersNewCtrl.$resolve = {
    equipmentCertOper: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('EquipmentOpersNewCtrl', EquipmentOpersNewCtrl);
}(angular));