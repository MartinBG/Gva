/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOpersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentCertOperational,
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

    $scope.deleteCertOper = function (certOper) {
      return EquipmentCertOperational.remove({ id: $stateParams.id, ind: certOper.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'EquipmentCertOperational',
    'certOpers'
  ];

  EquipmentOpersSearchCtrl.$resolve = {
    certOpers: [
      '$stateParams',
      'EquipmentCertOperational',
      function ($stateParams, EquipmentCertOperational) {
        return EquipmentCertOperational.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOpersSearchCtrl', EquipmentOpersSearchCtrl);
}(angular));
