/*global angular*/
(function (angular) {
  'use strict';

  function CertNoisesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoise,
    noises
  ) {
    $scope.noises = noises;


    $scope.editCertNoise = function (noise) {
      return $state.go('root.aircrafts.view.noises.edit', {
        id: $stateParams.id,
        ind: noise.partIndex
      });
    };

    $scope.deleteCertNoise = function (noise) {
      return AircraftCertNoise.remove({ id: $stateParams.id, ind: noise.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftCertNoise',
    'noises'
  ];

  CertNoisesSearchCtrl.$resolve = {
    noises: [
      '$stateParams',
      'AircraftCertNoise',
      function ($stateParams, AircraftCertNoise) {
        return AircraftCertNoise.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesSearchCtrl', CertNoisesSearchCtrl);
}(angular));
