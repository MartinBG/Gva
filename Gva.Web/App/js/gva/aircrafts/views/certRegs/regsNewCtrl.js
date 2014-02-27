/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistration,
    aircraftCertRegistration
  ) {
    $scope.isEdit = false;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      $scope.aircraftCertRegForm.$validate()
         .then(function () {
            if ($scope.aircraftCertRegForm.$valid) {
              return AircraftCertRegistration
              .save({ id: $stateParams.id }, $scope.reg).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.regs.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.regs.search');
    };
  }

  CertRegsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistration',
    'aircraftCertRegistration'
  ];
  CertRegsNewCtrl.$resolve = {
    aircraftCertRegistration: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertRegsNewCtrl', CertRegsNewCtrl);
}(angular));
