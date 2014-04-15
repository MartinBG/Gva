/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertRegsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    aircraftCertRegistration,
    oldReg
  ) {
    $scope.isEdit = false;

    $scope.reg = aircraftCertRegistration;

    if (oldReg) {
      _.defaults($scope.reg.part, _.cloneDeep(oldReg.part));
    }

    if ($state.payload) {
      $scope.reg.part.register = $state.payload.register;
      $scope.reg.part.certNumber = $state.payload.certNumber;
      $scope.reg.part.regMark = $state.payload.regMark;
    }

    $scope.save = function () {
      return $scope.newCertRegForm.$validate()
         .then(function () {
            if ($scope.newCertRegForm.$valid) {
              return AircraftCertRegistrationFM
              .save({ id: $stateParams.id }, $scope.reg).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.regsFM.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };
  }

  CertRegsFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationFM',
    'aircraftCertRegistration',
    'oldReg'
  ];
  CertRegsFMNewCtrl.$resolve = {
    aircraftCertRegistration: function () {
      return {
        part: {
          removalDate: null, // TODO HACK
          removalReason: null,
          removalText: null,
          removalDocumentNumber: null,
          removalDocumentDate: null,
          removalInspector: null,
          removalCountry: null,
          removalNotes: null,
          removalNotesAlt: null,
          isActive: true
        }
      };
    },
    oldReg: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.get({ id: $stateParams.id, ind: $stateParams.oldInd })
          .$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMNewCtrl', CertRegsFMNewCtrl);
}(angular, _));
