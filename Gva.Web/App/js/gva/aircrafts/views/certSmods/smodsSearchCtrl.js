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

    $scope.deleteCertSmod = function (smod) {
      return AircraftCertSmod.remove({ id: $stateParams.id, ind: smod.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
