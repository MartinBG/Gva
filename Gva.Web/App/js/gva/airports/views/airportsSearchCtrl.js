/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    airports) {

    $scope.filters = {
      name: null,
      icao: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.airports = airports;

    $scope.search = function () {
      $state.go('root.airports.search', {
        name: $scope.filters.name,
        icao: $scope.filters.icao
      });
    };

    $scope.newAirport = function () {
      return $state.go('root.airports.new');
    };

    $scope.viewAirport = function (airport) {
      return $state.go('root.airports.view', { id: airport.id });
    };
  }

  AirportsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'airports'
  ];

  AirportsSearchCtrl.$resolve = {
    airports: [
      '$stateParams',
      'Airport',
      function ($stateParams, Airport) {
        return Airport.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsSearchCtrl', AirportsSearchCtrl);
}(angular, _));
