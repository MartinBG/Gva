﻿/*global angular*/
(function (angular) {
  'use strict';

  function AirportsNewCtrl($scope, $state, Airports, airport) {
    $scope.airport = airport;

    $scope.save = function () {
      return $scope.newAirportForm.$validate()
      .then(function () {
        if ($scope.newAirportForm.$valid) {
          return Airports.save($scope.airport).$promise
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

  AirportsNewCtrl.$inject = ['$scope', '$state', 'Airports', 'airport'];

  AirportsNewCtrl.$resolve = {
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

  angular.module('gva').controller('AirportsNewCtrl', AirportsNewCtrl);
}(angular));
