/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    aircraftCertRegistration
  ) {
    $scope.isEdit = false;

    $scope.reg = aircraftCertRegistration;

    $scope.save = function () {
      $scope.aircraftCertRegForm.$validate()
         .then(function () {
            if ($scope.aircraftCertRegForm.$valid) {
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
    'aircraftCertRegistration'
  ];
  CertRegsFMNewCtrl.$resolve = {
    aircraftCertRegistration: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertRegsFMNewCtrl', CertRegsFMNewCtrl);
}(angular));
