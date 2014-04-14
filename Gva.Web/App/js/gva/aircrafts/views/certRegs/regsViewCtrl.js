/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsViewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationCurrentFM,
    AircraftCertRegistrationFM,
    AircraftCertAirworthinessFM,
    aircraftCertRegistrations,
    aircraftCertRegistration,
    aircraftCertAirworthiness,
    AircraftDocumentDebtFM,
    debts
  ) {
    $scope.isEdit = true;

    $scope.regs = aircraftCertRegistrations;
    $scope.reg = aircraftCertRegistration;
    $scope.aw = aircraftCertAirworthiness;
    if (!aircraftCertRegistrations.notRegistered) {
      $stateParams.ind = aircraftCertRegistration.partIndex;
    }
    $scope.debts = debts;

    $scope.switchReg = function (ind) {
      return $state.go($state.current,
        {
          id: $stateParams.id,
          ind: ind
        },
        {
          reload: true
        });
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
    
    $scope.newReg = function () {
      return $state.go('root.aircrafts.view.regsFM.new');
    };

  }

  CertRegsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationCurrentFM',
    'AircraftCertRegistrationFM',
    'AircraftCertAirworthinessFM',
    'aircraftCertRegistrations',
    'aircraftCertRegistration',
    'aircraftCertAirworthiness',
    'AircraftDocumentDebtFM',
    'debts'
  ];

  CertRegsViewCtrl.$resolve = {
    aircraftCertRegistrations: [
      '$stateParams',
      'AircraftCertRegistrationCurrentFM',
      function ($stateParams, AircraftCertRegistrationCurrentFM) {
        return AircraftCertRegistrationCurrentFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      'aircraftCertRegistrations',
      function ($stateParams, AircraftCertRegistrationFM, aircraftCertRegistrations) {
        if (!aircraftCertRegistrations.notRegistered) {
          return AircraftCertRegistrationFM.get({
            id: $stateParams.id,
            ind: aircraftCertRegistrations.currentIndex
          }).$promise;
        }
        else {
          return undefined;
        }
      }
    ],
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessFM',
      'aircraftCertRegistrations',
      function ($stateParams, AircraftCertAirworthinessFM, aircraftCertRegistrations) {
        if (!aircraftCertRegistrations.notRegistered &&
          aircraftCertRegistrations.airworthinessIndex) {
          return AircraftCertAirworthinessFM.get({
            id: $stateParams.id,
            ind: aircraftCertRegistrations.airworthinessIndex
          }).$promise;
        }
        else {
          return undefined;
        }
      }
    ],
    debts: [
      '$stateParams',
      'AircraftDocumentDebtFM',
      function ($stateParams, AircraftDocumentDebtFM) {
        return AircraftDocumentDebtFM.query({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsViewCtrl', CertRegsViewCtrl);
}(angular));