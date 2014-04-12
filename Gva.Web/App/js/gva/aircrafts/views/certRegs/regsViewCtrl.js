/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsViewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationCurrentFM,
    AircraftCertRegistrationFM,
    aircraftCertRegistrations,
    aircraftCertRegistration,
    AircraftDocumentDebtFM,
    debts
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;
    $scope.regs = aircraftCertRegistrations;
    $stateParams.ind = aircraftCertRegistration.partIndex;
    $scope.debts = debts;

    $scope.switchReg = function (ind) {
      return $state.go($state.current,
        {
          id: $stateParams.id,
          ind: ind
        });
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };

  }

  CertRegsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationCurrentFM',
    'AircraftCertRegistrationFM',
    'aircraftCertRegistrations',
    'aircraftCertRegistration',
    'AircraftDocumentDebtFM',
    'debts'
  ];

  CertRegsViewCtrl.$resolve = {
    aircraftCertRegistrations: [
      '$stateParams',
      'AircraftCertRegistrationCurrentFM',
      function ($stateParams, AircraftCertRegistrationCurrentFM) {
        return AircraftCertRegistrationCurrentFM.get({
          id: $stateParams.id
        }).$promise;
      }
    ],
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      'aircraftCertRegistrations',
      function ($stateParams, AircraftCertRegistrationFM, aircraftCertRegistrations) {
        return AircraftCertRegistrationFM.get({
          id: $stateParams.id,
          ind: aircraftCertRegistrations.currentIndex
        }).$promise;
      }
    ],
    debts: [
      '$stateParams',
      'AircraftDocumentDebtFM',
      function ($stateParams, AircraftDocumentDebtFM) {
        return AircraftDocumentDebtFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsViewCtrl', CertRegsViewCtrl);
}(angular));