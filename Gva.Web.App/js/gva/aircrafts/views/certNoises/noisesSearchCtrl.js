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

    $scope.editCertNoise = function (noise) {
      return $state.go('root.aircrafts.view.noises.edit', {
        id: $stateParams.id,
        ind: noise.partIndex
      });
    };

    $scope.newCertNoise = function () {
      return $state.go('root.aircrafts.view.noises.new');
    };
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
