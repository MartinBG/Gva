/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoises,
    noises
  ) {
    $scope.noises = noises;
  }

  CertNoisesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoises',
    'noises'
  ];

  CertNoisesSearchCtrl.$resolve = {
    noises: [
      '$stateParams',
      'AircraftCertNoises',
      function ($stateParams, AircraftCertNoises) {
        return AircraftCertNoises.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesSearchCtrl', CertNoisesSearchCtrl);
}(angular));
