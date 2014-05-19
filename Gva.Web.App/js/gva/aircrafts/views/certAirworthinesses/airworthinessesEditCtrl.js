/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirworthinessesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthiness,
    aircraftCertAirworthiness
  ) {
    var originalAirworthiness = _.cloneDeep(aircraftCertAirworthiness);

    $scope.isEdit = true;
    $scope.aw = aircraftCertAirworthiness;
    $scope.editMode = null;

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
          return AircraftCertAirworthiness
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.airworthinesses.search');
            });
        }
      });
    };

    $scope.deleteAirworthiness = function () {
      return AircraftCertAirworthiness.remove({
        id: $stateParams.id,
        ind: aircraftCertAirworthiness.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.airworthinesses.search');
        });
    };
  }

  CertAirworthinessesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthiness',
    'aircraftCertAirworthiness'
  ];

  CertAirworthinessesEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthiness',
      function ($stateParams, AircraftCertAirworthiness) {
        return AircraftCertAirworthiness.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesEditCtrl', CertAirworthinessesEditCtrl);
}(angular));