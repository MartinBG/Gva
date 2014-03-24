﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    regs
  ) {
    $scope.regs = regs;


    $scope.editCertReg = function (reg) {
      return $state.go('root.aircrafts.view.regsFM.edit', {
        id: $stateParams.id,
        ind: reg.partIndex
      });
    };

    $scope.deleteCertReg = function (reg) {
      return AircraftCertRegistrationFM.remove({ id: $stateParams.id, ind: reg.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newCertReg = function () {
      return $state.go('root.aircrafts.view.regsFM.new');
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
}(angular));