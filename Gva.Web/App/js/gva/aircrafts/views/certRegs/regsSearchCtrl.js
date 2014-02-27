/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistration,
    regs
  ) {
    $scope.regs = regs;


    $scope.editCertReg = function (reg) {
      return $state.go('root.aircrafts.view.regs.edit', {
        id: $stateParams.id,
        ind: reg.partIndex
      });
    };

    $scope.deleteCertReg = function (reg) {
      return AircraftCertRegistration.remove({ id: $stateParams.id, ind: reg.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newCertReg = function () {
      return $state.go('root.aircrafts.view.regs.new');
    };
  }

  CertRegsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistration',
    'regs'
  ];

  CertRegsSearchCtrl.$resolve = {
    regs: [
      '$stateParams',
      'AircraftCertRegistration',
      function ($stateParams, AircraftCertRegistration) {
        return AircraftCertRegistration.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsSearchCtrl', CertRegsSearchCtrl);
}(angular));
