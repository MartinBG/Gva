/*global angular*/
(function (angular) {
  'use strict';

  function CertRadiosNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadios,
    aircraftCertRadio
  ) {
    $scope.isEdit = false;

    $scope.radio = aircraftCertRadio;

    $scope.save = function () {
      return $scope.newCertRadioForm.$validate()
         .then(function () {
            if ($scope.newCertRadioForm.$valid) {
              return AircraftCertRadios
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
    'AircraftCertRadios',
    'aircraftCertRadio'
  ];
  CertRadiosNewCtrl.$resolve = {
    aircraftCertRadio: function () {
      return {
        part: {},
        applications: []
      };
    }
  };

  angular.module('gva').controller('CertRadiosNewCtrl', CertRadiosNewCtrl);
}(angular));
