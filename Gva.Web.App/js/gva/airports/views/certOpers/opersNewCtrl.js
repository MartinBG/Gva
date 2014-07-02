/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportCertOperationals,
    airportCertOper
  ) {
    $scope.save = function () {
      return $scope.newCertOperForm.$validate()
        .then(function () {
          if ($scope.newCertOperForm.$valid) {
            return AirportCertOperationals
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
    'AirportCertOperationals',
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