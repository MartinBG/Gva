/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    aircraftCertRegistration
  ) {
    $scope.isEdit = true;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      $scope.aircraftCertRegForm.$validate()
      .then(function () {
        if ($scope.aircraftCertRegForm.$valid) {
          return AircraftCertRegistrationFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
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

  CertRegsFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationFM',
    'aircraftCertRegistration'
  ];

  CertRegsFMEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMEditCtrl', CertRegsFMEditCtrl);
}(angular));