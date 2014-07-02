/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportSelectCtrl($scope, $state, $stateParams, Airports, selectedAirport) {
    $scope.filters = {
      name: null,
      icao: null
    };

    _.forOwn(_.pick($stateParams, ['name', 'icao']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    Airports.query($scope.filters).$promise.then(function (airports) {
      $scope.airports = airports;
    });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectAirport = function (result) {
      selectedAirport.push(result.id);
      return $state.go('^');
    };

    $scope.viewAirport = function (result) {
      return $state.go('root.airports.view.edit', { id: result.id });
    };
  }

  AirportSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Airports',
    'selectedAirport'
  ];

  angular.module('gva').controller('AirportSelectCtrl', AirportSelectCtrl);
}(angular, _));
