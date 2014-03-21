/*global angular*/
(function (angular) {
  'use strict';

  function CertRadiosNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadio,
    aircraftCertRadio
  ) {
    $scope.isEdit = false;

    $scope.radio = aircraftCertRadio;

    $scope.save = function () {
      return $scope.aircraftCertRadioForm.$validate()
         .then(function () {
            if ($scope.aircraftCertRadioForm.$valid) {
              return AircraftCertRadio
              .save({ id: $stateParams.id }, $scope.radio).$promise
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

  CertRadiosNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRadio',
    'aircraftCertRadio'
  ];
  CertRadiosNewCtrl.$resolve = {
    aircraftCertRadio: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertRadiosNewCtrl', CertRadiosNewCtrl);
}(angular));
