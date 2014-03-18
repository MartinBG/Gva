/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoiseFM,
    aircraftCertNoise
  ) {
    $scope.isEdit = true;

    $scope.noise = aircraftCertNoise;

    $scope.save = function () {
      $scope.aircraftCertNoiseForm.$validate()
      .then(function () {
        if ($scope.aircraftCertNoiseForm.$valid) {
          return AircraftCertNoiseFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.noise)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.noisesFM.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.noisesFM.search');
    };
  }

  CertNoisesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoiseFM',
    'aircraftCertNoise'
  ];

  CertNoisesFMEditCtrl.$resolve = {
    aircraftCertNoise: [
      '$stateParams',
      'AircraftCertNoiseFM',
      function ($stateParams, AircraftCertNoiseFM) {
        return AircraftCertNoiseFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesFMEditCtrl', CertNoisesFMEditCtrl);
}(angular));