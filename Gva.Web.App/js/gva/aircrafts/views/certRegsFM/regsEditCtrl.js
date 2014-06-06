/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRegsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationFM,
    aircraftCertRegistration
  ) {
    var originalRegistration = _.cloneDeep(aircraftCertRegistration);

    $scope.isEdit = true;
    $scope.reg = aircraftCertRegistration;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };
    $scope.rereg = function () {
      return $state.go('root.aircrafts.view.regsFM.newWizzard', {
        oldInd: aircraftCertRegistration.partIndex
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.reg = _.cloneDeep(originalRegistration);
    };
    $scope.removeDereg = function () {
      $scope.reg.part.isActive = true;
      $scope.reg.part.removal = undefined;
    };

    $scope.save = function () {
      return $scope.editCertRegForm.$validate()
      .then(function () {
        if ($scope.editCertRegForm.$valid) {
          return AircraftCertRegistrationFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regsFM.search');
            });
        }
      });
    };

    $scope.deleteReg = function () {
      return AircraftCertRegistrationFM.remove({
        id: $stateParams.id,
        ind: aircraftCertRegistration.partIndex
      })
          .$promise.then(function () {
        return $state.go('root.aircrafts.view.regsFM.search');
      });
    };

    $scope.back = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };

    $scope.dereg = function () {
      return $state.go('root.aircrafts.view.regsFM.dereg', {
        id: $stateParams.id,
        ind: $stateParams.ind
      });
    };
  }

  CertRegsFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationFM',
    'aircraftCertRegistration'
  ];

  CertRegsFMEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        return AircraftCertRegistrationFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMEditCtrl', CertRegsFMEditCtrl);
}(angular));