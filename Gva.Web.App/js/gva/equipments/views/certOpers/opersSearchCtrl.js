/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOpersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certOpers
  ) {
    $scope.certOpers = certOpers;

    $scope.editCertOper = function (certOper) {
      return $state.go('root.equipments.view.opers.edit',
        {
          id: $stateParams.id,
          ind: certOper.partIndex
        });
    };

    $scope.newCertOper = function () {
      return $state.go('root.equipments.view.opers.new');
    };
  }

  EquipmentOpersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certOpers'
  ];

  EquipmentOpersSearchCtrl.$resolve = {
    certOpers: [
      '$stateParams',
      'EquipmentCertOperationals',
      function ($stateParams, EquipmentCertOperationals) {
        return EquipmentCertOperationals.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOpersSearchCtrl', EquipmentOpersSearchCtrl);
}(angular));
