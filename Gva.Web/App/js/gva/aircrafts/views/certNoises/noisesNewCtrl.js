/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoise,
    aircraftCertNoise
  ) {
    $scope.isEdit = false;

    $scope.noise = aircraftCertNoise;

    $scope.save = function () {
      return $scope.newCertNoiseForm.$validate()
         .then(function () {
            if ($scope.newCertNoiseForm.$valid) {
              return AircraftCertNoise
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
    'AircraftCertNoise',
    'aircraftCertNoise'
  ];
  CertNoisesNewCtrl.$resolve = {
    aircraftCertNoise: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertNoisesNewCtrl', CertNoisesNewCtrl);
}(angular));
