/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthiness,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = true;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      return $scope.aircraftCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.aircraftCertAirworthinessForm.$valid) {
          return AircraftCertAirworthiness
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.airworthinesses.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinesses.search');
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