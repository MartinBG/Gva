/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportSelectCtrl($scope, $state, $stateParams, Airport, selectedAirport) {
    $scope.filters = {
      name: null,
      icao: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Airport.query($stateParams).$promise.then(function (airports) {
      $scope.airports = airports;
    });

    $scope.search = function () {
      $state.go($state.current, {
        name: $scope.filters.name,
        icao: $scope.filters.icao
      }, { reload: true });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectAirport = function (result) {
      selectedAirport.push(result.id);
      return $state.go('^');
    };
  }

  AirportSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Airport',
    'selectedAirport'
  ];

  angular.module('gva').controller('AirportSelectCtrl', AirportSelectCtrl);
}(angular, _));
