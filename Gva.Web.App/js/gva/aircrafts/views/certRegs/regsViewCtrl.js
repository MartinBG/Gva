/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsViewCtrl(
    $scope,
    $state,
    $stateParams,
    aircraftCertRegistrationView,
    aircraftCertRegistration,
    aircraftCertAirworthiness,
    debts
  ) {
    $scope.isEdit = true;

    $scope.regView = aircraftCertRegistrationView;
    $scope.reg = aircraftCertRegistration;
    $scope.aw = aircraftCertAirworthiness;
    $scope.debts = debts;

    if ($scope.regView) {
      $stateParams.ind = $scope.regView.currentIndex;
    }

    $scope.editDocumentDebt = function (debt) {
      return $state.go('root.aircrafts.view.debtsFM.edit', {
        id: $stateParams.id,
        ind: debt.partIndex
      });
    };

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
      return $state.go('root.aircrafts.view.regsFM.newWizzard');
    };
  }

  CertRegsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'aircraftCertRegistrationView',
    'aircraftCertRegistration',
    'aircraftCertAirworthiness',
    'debts'
  ];

  CertRegsViewCtrl.$resolve = {
    aircraftCertRegistrationView: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.getView({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      'aircraftCertRegistrationView',
      function ($stateParams, AircraftCertRegistrationFM, aircraftCertRegistrationView) {
        if (aircraftCertRegistrationView) {
          return AircraftCertRegistrationFM.get({
            id: $stateParams.id,
            ind: aircraftCertRegistrationView.currentIndex
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
      'aircraftCertRegistrationView',
      function ($stateParams, AircraftCertAirworthinessFM, aircraftCertRegistrationView) {
        if (aircraftCertRegistrationView &&
          aircraftCertRegistrationView.airworthinessIndex) {
          return AircraftCertAirworthinessFM.get({
            id: $stateParams.id,
            ind: aircraftCertRegistrationView.airworthinessIndex
          }).$promise;
        }
        else {
          return undefined;
        }
      }
    ],
    debts: [
      '$stateParams',
      'AircraftCurrentDebtFM',
      'aircraftCertRegistrationView',
      function ($stateParams, AircraftDocumentDebtFM, aircraftCertRegistrationView) {
        if (aircraftCertRegistrationView) {
          return AircraftDocumentDebtFM.query({
            id: $stateParams.id,
            ind: aircraftCertRegistrationView.currentIndex
          }).$promise;
        } else {
          return undefined;
        }
      }
    ]
  };

  angular.module('gva').controller('CertRegsViewCtrl', CertRegsViewCtrl);
}(angular));