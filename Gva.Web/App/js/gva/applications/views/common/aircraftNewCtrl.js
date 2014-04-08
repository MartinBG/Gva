/*global angular*/
(function (angular) {
  'use strict';

  function AircraftNewCtrl($scope, $state, Aircraft, aircraft, selectedAircraft) {
    $scope.aircraft = aircraft;

    $scope.save = function () {
      return $scope.newAircraftForm.$validate()
      .then(function () {
        if ($scope.newAircraftForm.$valid) {
          return Aircraft.save($scope.aircraft).$promise
            .then(function (aircraft) {
              selectedAircraft.push(aircraft.id);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  AircraftNewCtrl.$inject = ['$scope', '$state', 'Aircraft', 'aircraft', 'selectedAircraft'];

  AircraftNewCtrl.$resolve = {
    aircraft: function () {
      return {
        aircraftData: {
          caseTypes: [
            {
              nomValueId: 3
            }
          ]
        }
      };
    }
  };

  angular.module('gva').controller('AircraftNewCtrl', AircraftNewCtrl);
}(angular));
