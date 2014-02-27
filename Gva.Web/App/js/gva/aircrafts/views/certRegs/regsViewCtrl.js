/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsViewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistration,
    AircraftCertSmod,
    AircraftCertMark,
    AircraftCertAirworthiness,
    AircraftCertNoise,
    AircraftCertPermitToFly,
    AircraftCertRadio,
    aircraftCertRegistration,
    aircraftCertSmod,
    aircraftCertMark,
    aircraftCertAirworthiness,
    aircraftCertNoise,
    aircraftCertPermitToFly,
    aircraftCertRadio
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;
    $scope.smod = aircraftCertSmod;
    $scope.mark = aircraftCertMark;
    $scope.aw = aircraftCertAirworthiness;
    $scope.noise = aircraftCertNoise;
    $scope.permit = aircraftCertPermitToFly;
    $scope.radio = aircraftCertRadio;

    $scope.switchReg = function (ind) {
      return $state.go($state.current,
        {
          id: $stateParams.id,
          ind: ind
        });
    };
  }

  CertRegsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistration',
    'AircraftCertSmod',
    'AircraftCertMark',
    'AircraftCertAirworthiness',
    'AircraftCertNoise',
    'AircraftCertPermitToFly',
    'AircraftCertRadio',
    'aircraftCertRegistration',
    'aircraftCertSmod',
    'aircraftCertMark',
    'aircraftCertAirworthiness',
    'aircraftCertNoise',
    'aircraftCertPermitToFly',
    'aircraftCertRadio'
  ];

  CertRegsViewCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistration',
      function ($stateParams, AircraftCertRegistration) {
        return AircraftCertRegistration.get({
          id: $stateParams.id,
          ind: $stateParams.ind || 'current'
        }).$promise;
      }
    ],
    aircraftCertSmod: [
      '$stateParams',
      'AircraftCertSmod',
      function ($stateParams, AircraftCertSmod) {
        return AircraftCertSmod.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ],
    aircraftCertMark: [
      '$stateParams',
      'AircraftCertMark',
      function ($stateParams, AircraftCertMark) {
        return AircraftCertMark.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ],
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthiness',
      function ($stateParams, AircraftCertAirworthiness) {
        return AircraftCertAirworthiness.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ],
    aircraftCertNoise: [
      '$stateParams',
      'AircraftCertNoise',
      function ($stateParams, AircraftCertNoise) {
        return AircraftCertNoise.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ],
    aircraftCertPermitToFly: [
      '$stateParams',
      'AircraftCertPermitToFly',
      function ($stateParams, AircraftCertPermitToFly) {
        return AircraftCertPermitToFly.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ],
    aircraftCertRadio: [
      '$stateParams',
      'AircraftCertRadio',
      function ($stateParams, AircraftCertRadio) {
        return AircraftCertRadio.get({
          id: $stateParams.id,
          ind: 'current'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsViewCtrl', CertRegsViewCtrl);
}(angular));