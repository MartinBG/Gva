/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    aircrafts) {

    $scope.filters = {
      manSN: null,
      model: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.aircrafts = aircrafts;

    $scope.search = function () {
      $state.go('root.aircrafts.search', {
        manSN: $scope.filters.manSN,
        model: $scope.filters.model,
        icao: $scope.filters.icao
      });
    };

    $scope.newAircraft = function () {
      return $state.go('root.aircrafts.new');
    };

    $scope.viewAircraft = function (aircraft) {
      return $state.go('root.aircrafts.view', { id: aircraft.id });
    };
  }

  AircraftsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'aircrafts'
  ];

  AircraftsSearchCtrl.$resolve = {
    aircrafts: [
      '$stateParams',
      'Aircraft',
      function ($stateParams, Aircraft) {
        return Aircraft.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsSearchCtrl', AircraftsSearchCtrl);
}(angular, _));
