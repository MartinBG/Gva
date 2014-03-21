/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertSmod,
    aircraftCertSmod
  ) {
    $scope.isEdit = true;

    $scope.smod = aircraftCertSmod;

    $scope.save = function () {
      return $scope.aircraftCertSmodForm.$validate()
      .then(function () {
        if ($scope.aircraftCertSmodForm.$valid) {
          return AircraftCertSmod
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.smod)
            .$promise
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

  CertSmodsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertSmod',
    'aircraftCertSmod'
  ];

  CertSmodsEditCtrl.$resolve = {
    aircraftCertSmod: [
      '$stateParams',
      'AircraftCertSmod',
      function ($stateParams, AircraftCertSmod) {
        return AircraftCertSmod.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsEditCtrl', CertSmodsEditCtrl);
}(angular));