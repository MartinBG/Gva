/*global angular*/
(function (angular) {
  'use strict';

  function AirportsNewCtrl($scope, $state, Airport, airport) {
    $scope.airport = airport;

    $scope.save = function () {
      return $scope.newAirportForm.$validate()
      .then(function () {
        if ($scope.newAirportForm.$valid) {
          return Airport.save($scope.airport).$promise
            .then(function () {
              return $state.go('root.airports.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.search');
    };
  }

  AirportsNewCtrl.$inject = ['$scope', '$state', 'Airport', 'airport'];

  AirportsNewCtrl.$resolve = {
    airport: function () {
      return {
        airportData: {
          frequencies: [],
          radioNavigationAids: []
        }
      };
    }
  };

  angular.module('gva').controller('AirportsNewCtrl', AirportsNewCtrl);
}(angular));
