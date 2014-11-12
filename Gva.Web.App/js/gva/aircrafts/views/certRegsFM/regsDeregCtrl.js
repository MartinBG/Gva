/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMDeregCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationsFM,
    aircraftCertRegistration
  ) {
    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      return $scope.deregCertRegForm.$validate()
      .then(function () {
        if ($scope.deregCertRegForm.$valid) {
          return AircraftCertRegistrationsFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regsFM.search', {}, {reload: true});
            });
        }
      });
    };

    $scope.back = function () {
      return $state.go('root.aircrafts.view.regsFM.edit', {
        id: $stateParams.id,
        ind: $stateParams.ind
      });
    };
  }

  CertRegsFMDeregCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationsFM',
    'aircraftCertRegistration'
  ];

  CertRegsFMDeregCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        return AircraftCertRegistrationsFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise.then(function (reg) {
          reg.part.isActive = false;

          if (!reg.part.removal) {
            reg.part.removal = {};
          }
          reg.part.removal['export'] = null;

          return reg;
        });
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMDeregCtrl', CertRegsFMDeregCtrl);
}(angular));