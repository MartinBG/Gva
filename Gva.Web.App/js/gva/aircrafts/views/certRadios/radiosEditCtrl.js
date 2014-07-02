﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRadiosEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadios,
    aircraftCertRadio
  ) {
    var originalRadio = _.cloneDeep(aircraftCertRadio);

    $scope.isEdit = true;
    $scope.radio = aircraftCertRadio;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.radio = _.cloneDeep(originalRadio);
    };

    $scope.save = function () {
      return $scope.editCertRadioForm.$validate()
      .then(function () {
        if ($scope.editCertRadioForm.$valid) {
          return AircraftCertRadios
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.radio)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.radios.search');
            });
        }
      });
    };

    $scope.deleteRadio = function () {
      return AircraftCertRadios.remove({ id: $stateParams.id, ind: aircraftCertRadio.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.radios.search');
        });
    };
  }

  CertRadiosEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRadios',
    'aircraftCertRadio'
  ];

  CertRadiosEditCtrl.$resolve = {
    aircraftCertRadio: [
      '$stateParams',
      'AircraftCertRadios',
      function ($stateParams, AircraftCertRadios) {
        return AircraftCertRadios.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRadiosEditCtrl', CertRadiosEditCtrl);
}(angular));