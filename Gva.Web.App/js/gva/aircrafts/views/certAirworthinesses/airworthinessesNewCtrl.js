/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinesses,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = false;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
         .then(function () {
            if ($scope.newCertAirworthinessForm.$valid) {
              return AircraftCertAirworthinesses
              .save({ id: $stateParams.id }, $scope.aw).$promise
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

  CertAirworthinessesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinesses',
    'aircraftCertAirworthiness'
  ];
  CertAirworthinessesNewCtrl.$resolve = {
    aircraftCertAirworthiness: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertAirworthinessesNewCtrl', CertAirworthinessesNewCtrl);
}(angular));
