/*global angular*/
(function (angular) {
  'use strict';

  function AirportNewCtrl($scope, $state, Airport, airport, selectedAirport) {
    $scope.airport = airport;

    $scope.save = function () {
      return $scope.newAirportForm.$validate()
      .then(function () {
        if ($scope.newAirportForm.$valid) {
          return Airport.save($scope.airport).$promise
            .then(function (airport) {
              selectedAirport.push(airport.id);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  AirportNewCtrl.$inject = ['$scope', '$state', 'Airport', 'airport', 'selectedAirport'];

  AirportNewCtrl.$resolve = {
    airport: function () {
      return {
        airportData: {
          caseTypes: [
            {
              nomValueId: 4
            }
          ],
          frequencies: [],
          radioNavigationAids: []
        }
      };
    }
  };

  angular.module('gva').controller('AirportNewCtrl', AirportNewCtrl);
}(angular));
