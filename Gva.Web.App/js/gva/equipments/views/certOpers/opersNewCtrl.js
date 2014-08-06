/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOpersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperationals,
    equipmentCertOper
  ) {
    $scope.equipmentCertOper = equipmentCertOper;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newCertOperForm.$validate()
        .then(function () {
          if ($scope.newCertOperForm.$valid) {
            return EquipmentCertOperationals
              .save({ id: $stateParams.id }, $scope.equipmentCertOper).$promise
              .then(function () {
                return $state.go('root.equipments.view.opers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.opers.search');
    };
  }

  EquipmentOpersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentCertOperationals',
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