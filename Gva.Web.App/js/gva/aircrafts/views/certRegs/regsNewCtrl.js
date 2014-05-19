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
      return $scope.newCertRegForm.$validate()
         .then(function () {
            if ($scope.newCertRegForm.$valid) {
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
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertRegsNewCtrl', CertRegsNewCtrl);
}(angular));
