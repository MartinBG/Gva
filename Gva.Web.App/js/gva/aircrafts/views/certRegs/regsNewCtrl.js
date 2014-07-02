/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrations,
    aircraftCertRegistration
  ) {
    $scope.isEdit = false;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      return $scope.newCertRegForm.$validate()
         .then(function () {
            if ($scope.newCertRegForm.$valid) {
              return AircraftCertRegistrations
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
    'AircraftCertRegistrations',
    'aircraftCertRegistration'
  ];
  CertRegsNewCtrl.$resolve = {
    aircraftCertRegistration: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertRegsNewCtrl', CertRegsNewCtrl);
}(angular));
