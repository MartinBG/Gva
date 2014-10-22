/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRegsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrations,
    aircraftCertRegistration,
    scMessage
  ) {
    var originalRegistration = _.cloneDeep(aircraftCertRegistration);

    $scope.reg = aircraftCertRegistration;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

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
          return AircraftCertRegistrations
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regs.search');
            });
        }
      });
    };

    $scope.deleteCertReg = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertRegistrations.remove({
            id: $stateParams.id,
            ind: aircraftCertRegistration.partIndex
          }).$promise.then(function () {
              return $state.go('root.aircrafts.view.regs.search');
          });
        }
      });
    };
  }

  CertRegsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrations',
    'aircraftCertRegistration',
    'scMessage'
  ];

  CertRegsEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrations',
      function ($stateParams, AircraftCertRegistrations) {
        return AircraftCertRegistrations.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsEditCtrl', CertRegsEditCtrl);
}(angular));
