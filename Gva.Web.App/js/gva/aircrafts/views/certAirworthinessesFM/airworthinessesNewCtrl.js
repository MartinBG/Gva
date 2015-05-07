/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    airworthiness
  ) {
    $scope.airworthiness = airworthiness;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
        .then(function () {
          if ($scope.newCertAirworthinessForm.$valid) {
            return AircraftCertAirworthinessesFM
              .save({ id: $stateParams.id }, $scope.airworthiness)
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

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'airworthiness'
  ];

  CertAirworthinessesFMNewCtrl.$resolve = {
    airworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.newCertAirworthiness({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular));
