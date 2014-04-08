/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftSelectCtrl($scope, $state, $stateParams, Aircraft, selectedAircraft) {
    $scope.filters = {
      manSN: null,
      model: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Aircraft.query($stateParams).$promise.then(function (aircrafts) {
      $scope.aircrafts = aircrafts;
    });

    $scope.search = function () {
      $state.go('root.applications.new.aircraftSelect', {
        manSN: $scope.filters.manSN,
        model: $scope.filters.model,
        icao: $scope.filters.icao
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectAircraft = function (result) {
      selectedAircraft.push(result.id);
      return $state.go('^');
    };
  }

  AircraftSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircraft',
    'selectedAircraft'
  ];

  angular.module('gva').controller('AircraftSelectCtrl', AircraftSelectCtrl);
}(angular, _));
