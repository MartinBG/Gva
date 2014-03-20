/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = false;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      $scope.aircraftCertAirworthinessForm.$validate()
         .then(function () {
            if ($scope.aircraftCertAirworthinessForm.$valid) {
              return AircraftCertAirworthinessFM
              .save({ id: $stateParams.id }, $scope.aw).$promise
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

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'aircraftCertAirworthiness'
  ];
  CertAirworthinessesFMNewCtrl.$resolve = {
    aircraftCertAirworthiness: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular));
