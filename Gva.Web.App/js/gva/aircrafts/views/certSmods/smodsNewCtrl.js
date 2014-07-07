/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertSmods,
    aircraftCertSmod
  ) {
    $scope.isEdit = false;

    $scope.smod = aircraftCertSmod;

    $scope.save = function () {
      return $scope.newCertSmodForm.$validate()
         .then(function () {
            if ($scope.newCertSmodForm.$valid) {
              return AircraftCertSmods
              .save({ id: $stateParams.id }, $scope.smod).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.smods.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.smods.search');
    };
  }

  CertSmodsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertSmods',
    'aircraftCertSmod'
  ];
  CertSmodsNewCtrl.$resolve = {
    aircraftCertSmod: function () {
      return {
        part: {},
        applications: []
      };
    }
  };

  angular.module('gva').controller('CertSmodsNewCtrl', CertSmodsNewCtrl);
}(angular));
