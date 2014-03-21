/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = true;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      return $scope.aircraftCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.aircraftCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinessFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.airworthinessesFM.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.search');
    };
  }

  CertAirworthinessesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'aircraftCertAirworthiness'
  ];

  CertAirworthinessesFMEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessFM',
      function ($stateParams, AircraftCertAirworthinessFM) {
        return AircraftCertAirworthinessFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMEditCtrl', CertAirworthinessesFMEditCtrl);
}(angular));