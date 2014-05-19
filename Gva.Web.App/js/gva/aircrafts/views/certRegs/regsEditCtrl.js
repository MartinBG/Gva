/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRegsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistration,
    aircraftCertRegistration
  ) {
    var originalRegistration = _.cloneDeep(aircraftCertRegistration);

    $scope.isEdit = true;
    $scope.reg = aircraftCertRegistration;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.reg = _.cloneDeep(originalRegistration);
    };

    $scope.save = function () {
      return $scope.editCertRegForm.$validate()
      .then(function () {
        if ($scope.editCertRegForm.$valid) {
          return AircraftCertRegistration
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regs.search');
            });
        }
      });
    };

    $scope.deleteCertReg = function () {
      return AircraftCertRegistration.remove({
        id: $stateParams.id,
        ind: aircraftCertRegistration.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.regs.search');
        });
    };
  }

  CertRegsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistration',
    'aircraftCertRegistration'
  ];

  CertRegsEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistration',
      function ($stateParams, AircraftCertRegistration) {
        return AircraftCertRegistration.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsEditCtrl', CertRegsEditCtrl);
}(angular));