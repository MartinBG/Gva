/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoises,
    aircraftCertNoise
  ) {
    $scope.isEdit = false;

    $scope.noise = aircraftCertNoise;

    $scope.save = function () {
      return $scope.newCertNoiseForm.$validate()
         .then(function () {
            if ($scope.newCertNoiseForm.$valid) {
              return AircraftCertNoises
              .save({ id: $stateParams.id }, $scope.noise).$promise
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

  CertNoisesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoises',
    'aircraftCertNoise'
  ];

  CertNoisesNewCtrl.$resolve = {
    aircraftCertNoise: [
      '$stateParams',
      'AircraftCertNoises',
      function ($stateParams, AircraftCertNoises) {
        return AircraftCertNoises.newCertNoise({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesNewCtrl', CertNoisesNewCtrl);
}(angular));
