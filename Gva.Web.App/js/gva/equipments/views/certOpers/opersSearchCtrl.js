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
