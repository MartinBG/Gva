/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertRegsFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    regs
  ) {
    $scope.regs = regs.sort(function(a, b) {
      return b.part.actNumber-a.part.actNumber;
    });

    $scope.showNewButton = _.filter(regs, function (reg) {
      return reg.part.isActive;
    }).length < 1;
  }

  CertRegsFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'regs'
  ];

  CertRegsFMSearchCtrl.$resolve = {
    regs: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        return AircraftCertRegistrationsFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMSearchCtrl', CertRegsFMSearchCtrl);
}(angular, _));
