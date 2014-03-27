/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportCertOperational,
    airportCertOper
  ) {
    $scope.save = function () {
      return $scope.airportCertOperForm.$validate()
        .then(function () {
          if ($scope.airportCertOperForm.$valid) {
            return AirportCertOperational
              .save({ id: $stateParams.id }, $scope.airportCertOper).$promise
              .then(function () {
                return $state.go('root.airports.view.opers.search');
              });
          }
        });
    };

    $scope.airportCertOper = airportCertOper;

    $scope.cancel = function () {
      return $state.go('root.airports.view.opers.search');
    };
  }

  AirportOpersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportCertOperational',
    'airportCertOper'
  ];

  AirportOpersNewCtrl.$resolve = {
    airportCertOper: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('AirportOpersNewCtrl', AirportOpersNewCtrl);
}(angular));