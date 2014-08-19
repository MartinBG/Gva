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
      model: null,
      mark: null
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
        mark: $scope.filters.mark,
        airCategory: $scope.filters.airCategory,
        aircraftProducer: $scope.filters.aircraftProducer
      });
    };

    $scope.newAircraft = function () {
      return $state.go('root.aircrafts.newWizzard');
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
      'Aircrafts',
      function ($stateParams, Aircrafts) {
        return Aircrafts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsSearchCtrl', AircraftsSearchCtrl);
}(angular, _));
