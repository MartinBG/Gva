/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoise,
    aircraftCertNoise
  ) {
    $scope.isEdit = true;

    $scope.noise = aircraftCertNoise;

    $scope.save = function () {
      return $scope.aircraftCertNoiseForm.$validate()
      .then(function () {
        if ($scope.aircraftCertNoiseForm.$valid) {
          return AircraftCertNoise
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.noise)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.noises.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.noises.search');
    };
  }

  CertNoisesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoise',
    'aircraftCertNoise'
  ];

  CertNoisesEditCtrl.$resolve = {
    aircraftCertNoise: [
      '$stateParams',
      'AircraftCertNoise',
      function ($stateParams, AircraftCertNoise) {
        return AircraftCertNoise.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesEditCtrl', CertNoisesEditCtrl);
}(angular));