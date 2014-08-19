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
