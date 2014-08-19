/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    smods
  ) {
    $scope.smods = smods;
  }

  CertSmodsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'smods'
  ];

  CertSmodsSearchCtrl.$resolve = {
    smods: [
      '$stateParams',
      'AircraftCertSmods',
      function ($stateParams, AircraftCertSmods) {
        return AircraftCertSmods.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsSearchCtrl', CertSmodsSearchCtrl);
}(angular));
