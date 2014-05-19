/*global angular,_*/
(function (angular) {
  'use strict';

  function CertNoisesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoiseFM,
    aircraftCertNoise
  ) {
    var originalNoise = _.cloneDeep(aircraftCertNoise);

    $scope.isEdit = true;
    $scope.noise = aircraftCertNoise;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.noise = _.cloneDeep(originalNoise);
    };

    $scope.save = function () {
      return $scope.editCertNoiseForm.$validate()
          .then(function () {
        if ($scope.editCertNoiseForm.$valid) {
          return AircraftCertNoiseFM
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.noise)
              .$promise
              .then(function () {
            return $state.go('root.aircrafts.view.noisesFM.search');
          });
        }
      });
    };

    $scope.deleteNoise = function () {
      return AircraftCertNoiseFM.remove({
        id: $stateParams.id,
        ind: aircraftCertNoise.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.noisesFM.search');
        });
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