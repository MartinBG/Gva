/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftSelectCtrl($scope, $state, $stateParams, Aircrafts, selectedAircraft) {
    $scope.filters = {
      manSN: null,
      model: null
    };

    _.forOwn(_.pick($stateParams, ['manSN', 'model', 'icao']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    Aircrafts.query($scope.filters).$promise.then(function (aircrafts) {
      $scope.aircrafts = aircrafts;
    });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectAircraft = function (result) {
      selectedAircraft.push(result.id);
      return $state.go('^');
    };

    $scope.viewAircraft = function (result) {
      return $state.go('root.aircrafts.view.edit', { id: result.id });
    };
  }

  AircraftSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircrafts',
    'selectedAircraft'
  ];

  angular.module('gva').controller('AircraftSelectCtrl', AircraftSelectCtrl);
}(angular, _));
