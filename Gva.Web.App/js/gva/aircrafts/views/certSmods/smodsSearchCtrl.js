/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'smods'
  ];

  CertSmodsSearchCtrl.$resolve = {
    smods: [
      '$stateParams',
      'AircraftCertSmods',
      function ($stateParams, AircraftCertSmods) {
        return AircraftCertSmods.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsSearchCtrl', CertSmodsSearchCtrl);
}(angular));
