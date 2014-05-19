/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertRegsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    regs
  ) {
    $scope.regs = regs;
    $scope.showNewButton = _.filter(regs, function (reg) {
      return reg.part.isActive;
    }).length < 1;


    $scope.editCertReg = function (reg) {
      return $state.go('root.aircrafts.view.regsFM.edit', {
        id: $stateParams.id,
        ind: reg.partIndex
      });
    };

    $scope.newCertReg = function () {
      return $state.go('root.aircrafts.view.regsFM.newWizzard');
    };
  }

  CertRegsFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationFM',
    'regs'
  ];

  CertRegsFMSearchCtrl.$resolve = {
    regs: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMSearchCtrl', CertRegsFMSearchCtrl);
}(angular, _));
