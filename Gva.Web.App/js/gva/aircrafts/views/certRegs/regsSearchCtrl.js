/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrations,
    regs
  ) {
    $scope.regs = regs;

    $scope.editCertReg = function (reg) {
      return $state.go('root.aircrafts.view.regs.edit', {
        id: $stateParams.id,
        ind: reg.partIndex
      });
    };

    $scope.newCertReg = function () {
      return $state.go('root.aircrafts.view.regs.new');
    };
  }

  CertRegsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrations',
    'regs'
  ];

  CertRegsSearchCtrl.$resolve = {
    regs: [
      '$stateParams',
      'AircraftCertRegistrations',
      function ($stateParams, AircraftCertRegistrations) {
        return AircraftCertRegistrations.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsSearchCtrl', CertRegsSearchCtrl);
}(angular));
