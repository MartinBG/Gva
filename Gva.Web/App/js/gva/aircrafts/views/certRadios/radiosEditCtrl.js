/*global angular*/
(function (angular) {
  'use strict';

  function CertRadiosEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadio,
    aircraftCertRadio
  ) {
    $scope.isEdit = true;

    $scope.radio = aircraftCertRadio;

    $scope.save = function () {
      $scope.aircraftCertRadioForm.$validate()
      .then(function () {
        if ($scope.aircraftCertRadioForm.$valid) {
          return AircraftCertRadio
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.radio)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.radios.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.radios.search');
    };
  }

  CertRadiosEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRadio',
    'aircraftCertRadio'
  ];

  CertRadiosEditCtrl.$resolve = {
    aircraftCertRadio: [
      '$stateParams',
      'AircraftCertRadio',
      function ($stateParams, AircraftCertRadio) {
        return AircraftCertRadio.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRadiosEditCtrl', CertRadiosEditCtrl);
}(angular));