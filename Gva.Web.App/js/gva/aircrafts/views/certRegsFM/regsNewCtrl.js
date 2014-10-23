/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationsFM,
    aircraftCertRegistration
  ) {
    $scope.reg = aircraftCertRegistration;

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
    'AircraftCertRegistrationsFM',
    'aircraftCertRegistration'
  ];
  CertRegsFMNewCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        return AircraftCertRegistrationsFM.newCertRegistrationFM({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMNewCtrl', CertRegsFMNewCtrl);
}(angular));
