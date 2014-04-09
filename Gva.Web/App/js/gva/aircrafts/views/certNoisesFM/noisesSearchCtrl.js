/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoiseFM,
    noises
  ) {
    $scope.noises = noises;

    $scope.editCertNoise = function (noise) {
      return $state.go('root.aircrafts.view.noisesFM.edit', {
        id: $stateParams.id,
        ind: noise.partIndex
      });
    };

    $scope.newCertNoise = function () {
      return $state.go('root.aircrafts.view.noisesFM.new');
    };
  }

  CertNoisesFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoiseFM',
    'noises'
  ];

  CertNoisesFMSearchCtrl.$resolve = {
    noises: [
      '$stateParams',
      'AircraftCertNoiseFM',
      function ($stateParams, AircraftCertNoiseFM) {
        return AircraftCertNoiseFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesFMSearchCtrl', CertNoisesFMSearchCtrl);
}(angular));
