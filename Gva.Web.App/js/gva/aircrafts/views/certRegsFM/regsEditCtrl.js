/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRegsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRegistrationsFM,
    aircraftCertRegistration,
    scMessage
  ) {
    var originalRegistration = _.cloneDeep(aircraftCertRegistration);

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
      return AircraftCertRegistrationsFM
        .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
        .$promise
        .then(function () {
          return $state.go($state.current, $stateParams, { reload: true });
        });
    };

    $scope.save = function () {
      return $scope.editCertRegForm.$validate()
      .then(function () {
        if ($scope.editCertRegForm.$valid) {
          return AircraftCertRegistrationsFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.regsFM.search', {}, {reload: true});
            });
        }
      });
    };

    $scope.deleteReg = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertRegistrationsFM.remove({
            id: $stateParams.id,
            ind: aircraftCertRegistration.partIndex
          })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.regsFM.search', {}, {reload: true});
          });
        }
      });
    };

    $scope.back = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };

    $scope.dereg = function () {
      return $state.go('root.aircrafts.view.regsFM.dereg', {
        id: $stateParams.id,
        ind: $stateParams.ind
      }, {reload: true});
    };
  }

  CertRegsFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRegistrationsFM',
    'aircraftCertRegistration',
    'scMessage'
  ];

  CertRegsFMEditCtrl.$resolve = {
    aircraftCertRegistration: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        return AircraftCertRegistrationsFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRegsFMEditCtrl', CertRegsFMEditCtrl);
}(angular));
