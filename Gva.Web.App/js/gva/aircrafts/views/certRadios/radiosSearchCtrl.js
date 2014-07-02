/*global angular*/
(function (angular) {
  'use strict';

  function CertRadiosSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'radios'
  ];

  CertRadiosSearchCtrl.$resolve = {
    radios: [
      '$stateParams',
      'AircraftCertRadios',
      function ($stateParams, AircraftCertRadios) {
        return AircraftCertRadios.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRadiosSearchCtrl', CertRadiosSearchCtrl);
}(angular));
