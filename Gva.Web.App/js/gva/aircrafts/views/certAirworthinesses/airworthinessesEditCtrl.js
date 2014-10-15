/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirworthinessesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinesses,
    aircraftCertAirworthiness,
    scMessage
  ) {
    var originalAirworthiness = _.cloneDeep(aircraftCertAirworthiness);

    $scope.aw = aircraftCertAirworthiness;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aw = _.cloneDeep(originalAirworthiness);
    };

    $scope.save = function () {
      return $scope.editCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.editCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinesses
          .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
          .$promise
          .then(function () {
            return $state.go('root.aircrafts.view.airworthinesses.search');
          });
        }
      });
    };

    $scope.deleteAirworthiness = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertAirworthinesses.remove({
            id: $stateParams.id,
            ind: aircraftCertAirworthiness.partIndex
          }).$promise.then(function () {
              return $state.go('root.aircrafts.view.airworthinesses.search');
            });
        }
      });
    };
  }

  CertAirworthinessesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinesses',
    'aircraftCertAirworthiness',
    'scMessage'
  ];

  CertAirworthinessesEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinesses',
      function ($stateParams, AircraftCertAirworthinesses) {
        return AircraftCertAirworthinesses.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesEditCtrl', CertAirworthinessesEditCtrl);
}(angular));
