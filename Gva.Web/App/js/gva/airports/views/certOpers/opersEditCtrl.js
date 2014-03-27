/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportCertOperational,
    airportCertOper
  ) {

    $scope.airportCertOper = airportCertOper;

    $scope.save = function () {
      return $scope.airportCertOperForm.$validate()
        .then(function () {
          if ($scope.airportCertOperForm.$valid) {
            return AirportCertOperational
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportCertOper)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.opers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.view.opers.search');
    };
  }

  AirportOpersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportCertOperational',
    'airportCertOper'
  ];

  AirportOpersEditCtrl.$resolve = {
    airportCertOper: [
      '$stateParams',
      'AirportCertOperational',
      function ($stateParams, AirportCertOperational) {
        return AirportCertOperational.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOpersEditCtrl', AirportOpersEditCtrl);
}(angular));