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
      return $scope.newCertRadioForm.$validate()
         .then(function () {
            if ($scope.newCertRadioForm.$valid) {
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
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertRadiosNewCtrl', CertRadiosNewCtrl);
}(angular));
