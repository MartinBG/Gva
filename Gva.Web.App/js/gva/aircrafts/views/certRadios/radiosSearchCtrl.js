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
