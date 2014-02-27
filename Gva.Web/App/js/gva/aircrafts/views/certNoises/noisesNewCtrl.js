﻿/*global angular*/
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
      $scope.aircraftCertNoiseForm.$validate()
         .then(function () {
            if ($scope.aircraftCertNoiseForm.$valid) {
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
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertNoisesNewCtrl', CertNoisesNewCtrl);
}(angular));
