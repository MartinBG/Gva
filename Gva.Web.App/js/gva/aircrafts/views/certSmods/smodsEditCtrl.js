/*global angular,_*/
(function (angular) {
  'use strict';

  function CertSmodsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertSmods,
    aircraftCertSmod,
    scMessage
  ) {
    var originalSmod = _.cloneDeep(aircraftCertSmod);
    $scope.lotId = $stateParams.id;
    $scope.smod = aircraftCertSmod;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.smod = _.cloneDeep(originalSmod);
    };

    $scope.save = function () {
      return $scope.editCertSmodForm.$validate()
      .then(function () {
        if ($scope.editCertSmodForm.$valid) {
          return AircraftCertSmods
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.smod)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.smods.search');
            });
        }
      });
    };

    $scope.deleteSmod = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertSmods.remove({ id: $stateParams.id, ind: aircraftCertSmod.partIndex })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.smods.search');
          });
        }
      });
    };
  }

  CertSmodsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertSmods',
    'aircraftCertSmod',
    'scMessage'
  ];

  CertSmodsEditCtrl.$resolve = {
    aircraftCertSmod: [
      '$stateParams',
      'AircraftCertSmods',
      function ($stateParams, AircraftCertSmods) {
        return AircraftCertSmods.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsEditCtrl', CertSmodsEditCtrl);
}(angular));
