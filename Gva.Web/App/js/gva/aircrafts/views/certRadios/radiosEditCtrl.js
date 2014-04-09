/*global angular,_*/
(function (angular) {
  'use strict';

  function CertRadiosEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertRadio,
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
          return AircraftCertRadio
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.radio)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.radios.search');
            });
        }
      });
    };

    $scope.deleteRadio = function () {
      return AircraftCertRadio.remove({ id: $stateParams.id, ind: aircraftCertRadio.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.radios.search');
        });
    };
  }

  CertRadiosEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertRadio',
    'aircraftCertRadio'
  ];

  CertRadiosEditCtrl.$resolve = {
    aircraftCertRadio: [
      '$stateParams',
      'AircraftCertRadio',
      function ($stateParams, AircraftCertRadio) {
        return AircraftCertRadio.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertRadiosEditCtrl', CertRadiosEditCtrl);
}(angular));