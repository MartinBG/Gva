/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationsFM,
    aircraftCertRegistration,
    oldReg
  ) {
    $scope.isEdit = false;

    $scope.reg = aircraftCertRegistration;

    if (oldReg && oldReg.part) {
      oldReg.part.isActive = false;
      oldReg.part.isCurrent = false;
    }

    if ($state.payload) {
      $scope.reg.part.register = $state.payload.register;
      $scope.reg.part.certNumber = $state.payload.certNumber;
      $scope.reg.part.actNumber = $state.payload.actNumber;
      $scope.reg.part.regMark = $state.payload.regMark;
    } else {
      return $state.go('root.aircrafts.view.regsFM.search');
    }

    $scope.save = function () {
      return $scope.newCertRegForm.$validate()
         .then(function () {
            if ($scope.newCertRegForm.$valid) {
              return AircraftCertRegistrationsFM
              .save({ id: $stateParams.id }, $scope.reg).$promise
              .then(function () {
                if (oldReg && oldReg.part) {
                  return AircraftCertRegistrationsFM
                  .save({ id: $stateParams.id, ind: oldReg.partIndex }, oldReg).$promise
                  .then(function () {
                    return $state.go('root.aircrafts.view.regsFM.search');
                  });
                } else {
                  return $state.go('root.aircrafts.view.regsFM.search');
                }
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
    'AircraftCertRegistrationsFM',
    'aircraftCertRegistration',
    'oldReg'
  ];
  CertRegsFMNewCtrl.$resolve = {
    aircraftCertRegistration: function () {
      return {
        part: {
          removal: null,
          isActive: true,
          isCurrent: true,
          ownerIsOrg: true,
          operIsOrg: true,
          lessorIsOrg: true
        },
        files: []
      };
    },
    oldReg: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        if ($stateParams.oldInd) {
          return AircraftCertRegistrationsFM.get({ id: $stateParams.id, ind: $stateParams.oldInd })
            .$promise;
        }
        else {
          return null;
        }
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMNewCtrl', CertRegsFMNewCtrl);
}(angular));
