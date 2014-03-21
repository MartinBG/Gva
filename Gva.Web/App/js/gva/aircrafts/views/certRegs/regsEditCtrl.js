/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistration,
    aircraftCertRegistration
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      return $scope.aircraftCertRegForm.$validate()
      .then(function () {
        if ($scope.aircraftCertRegForm.$valid) {
          return AircraftCertRegistration
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
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

  CertRegsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistration',
    'aircraftCertRegistration'
  ];

  CertRegsEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistration',
      function ($stateParams, AircraftCertRegistration) {
        return AircraftCertRegistration.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsEditCtrl', CertRegsEditCtrl);
}(angular));