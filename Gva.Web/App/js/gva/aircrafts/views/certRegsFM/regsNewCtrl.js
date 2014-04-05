﻿/*global angular*/
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
      return $scope.newCertRegForm.$validate()
         .then(function () {
            if ($scope.newCertRegForm.$valid) {
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
        part: {
          isActive: true
        }
      };
    }
  };

  angular.module('gva').controller('CertRegsFMNewCtrl', CertRegsFMNewCtrl);
}(angular));
