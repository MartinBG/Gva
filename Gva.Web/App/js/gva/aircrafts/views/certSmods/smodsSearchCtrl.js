/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertSmod,
    smods
  ) {
    $scope.smods = smods;

    $scope.editCertSmod = function (smod) {
      return $state.go('root.aircrafts.view.smods.edit', {
        id: $stateParams.id,
        ind: smod.partIndex
      });
    };

    $scope.newCertSmod = function () {
      return $state.go('root.aircrafts.view.smods.new');
    };
  }

  CertSmodsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertSmod',
    'smods'
  ];

  CertSmodsSearchCtrl.$resolve = {
    smods: [
      '$stateParams',
      'AircraftCertSmod',
      function ($stateParams, AircraftCertSmod) {
        return AircraftCertSmod.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsSearchCtrl', CertSmodsSearchCtrl);
}(angular));
