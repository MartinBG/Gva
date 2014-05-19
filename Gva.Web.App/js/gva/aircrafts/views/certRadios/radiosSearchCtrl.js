/*global angular*/
(function (angular) {
  'use strict';

  function CertRadiosSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadio,
    radios
  ) {
    $scope.radios = radios;

    $scope.editCertRadio = function (radio) {
      return $state.go('root.aircrafts.view.radios.edit', {
        id: $stateParams.id,
        ind: radio.partIndex
      });
    };

    $scope.newCertRadio = function () {
      return $state.go('root.aircrafts.view.radios.new');
    };
  }

  CertRadiosSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRadio',
    'radios'
  ];

  CertRadiosSearchCtrl.$resolve = {
    radios: [
      '$stateParams',
      'AircraftCertRadio',
      function ($stateParams, AircraftCertRadio) {
        return AircraftCertRadio.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRadiosSearchCtrl', CertRadiosSearchCtrl);
}(angular));
