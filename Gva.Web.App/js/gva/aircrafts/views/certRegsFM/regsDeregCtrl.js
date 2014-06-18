/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMDeregCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    aircraftCertRegistration
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      return $scope.deregCertRegForm.$validate()
      .then(function () {
        if ($scope.deregCertRegForm.$valid) {
          return AircraftCertRegistrationFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regsFM.search');
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
    'AircraftCertRegistrationFM',
    'aircraftCertRegistration'
  ];

  CertRegsFMDeregCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise.then(function (reg) {
          reg.part.isActive = false;
          reg.part.removal['export'] = null;
          return reg;
        });
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMDeregCtrl', CertRegsFMDeregCtrl);
}(angular));