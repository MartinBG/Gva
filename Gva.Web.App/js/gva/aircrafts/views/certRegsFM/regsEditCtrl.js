/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRegsFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    Aircrafts,
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
      return Aircrafts.getNextActNumber({
        registerId: aircraftCertRegistration.part.register.nomValueId
      }).$promise.then(function (result) {
        aircraftCertRegistration.part.actNumber = result.actNumber;
        return $state.go('root.aircrafts.view.regsFM.new',
          { oldInd: aircraftCertRegistration.partIndex },
          {},
          aircraftCertRegistration.part);
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.reg = _.cloneDeep(originalRegistration);
    };

    $scope.removeDereg = function () {
      return AircraftCertRegistrationsFM
        .removeDereg({ id: $stateParams.id, ind: $stateParams.ind }, $scope.reg)
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
            ind: $stateParams.ind
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
    'Aircrafts',
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
