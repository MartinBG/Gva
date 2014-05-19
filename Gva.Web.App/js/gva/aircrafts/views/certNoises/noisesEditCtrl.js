/*global angular,_*/
(function (angular) {
  'use strict';

  function CertNoisesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoise,
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
          return AircraftCertNoise
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.noise)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.noises.search');
            });
        }
      });
    };

    $scope.deleteNoise = function () {
      return AircraftCertNoise.remove({ id: $stateParams.id, ind: aircraftCertNoise.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.noises.search');
        });
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