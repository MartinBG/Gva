/*global angular,_*/
(function (angular) {
  'use strict';

  function CertNoisesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertNoises,
    aircraftCertNoise,
    scMessage
  ) {
    var originalNoise = _.cloneDeep(aircraftCertNoise);

    $scope.lotId = $stateParams.id;
    $scope.partIndex = $stateParams.ind;
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
          return AircraftCertNoises
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.noise)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.noises.search');
            });
        }
      });
    };

    $scope.deleteNoise = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertNoises
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.noises.search');
          });
        }
      });
    };
  }

  CertNoisesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertNoises',
    'aircraftCertNoise',
    'scMessage'
  ];

  CertNoisesEditCtrl.$resolve = {
    aircraftCertNoise: [
      '$stateParams',
      'AircraftCertNoises',
      function ($stateParams, AircraftCertNoises) {
        return AircraftCertNoises.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertNoisesEditCtrl', CertNoisesEditCtrl);
}(angular));
