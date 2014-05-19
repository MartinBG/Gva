/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoiseFM,
    aircraftCertNoise
  ) {
    $scope.isEdit = false;

    $scope.noise = aircraftCertNoise;

    $scope.save = function () {
      return $scope.newCertNoiseForm.$validate()
         .then(function () {
            if ($scope.newCertNoiseForm.$valid) {
              return AircraftCertNoiseFM
              .save({ id: $stateParams.id }, $scope.noise).$promise
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

  CertNoisesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoiseFM',
    'aircraftCertNoise'
  ];
  CertNoisesFMNewCtrl.$resolve = {
    aircraftCertNoise: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertNoisesFMNewCtrl', CertNoisesFMNewCtrl);
}(angular));
