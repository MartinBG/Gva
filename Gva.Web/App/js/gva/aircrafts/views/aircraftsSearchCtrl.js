/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Aircraft,
    aircrafts) {

    $scope.filters = {};

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.aircrafts = aircrafts;

    $scope.search = function () {
      $state.go('root.aircrafts.search', {
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
    'Aircraft',
    'aircrafts'
  ];

  AircraftsSearchCtrl.$resolve = {
    aircrafts: [
      '$stateParams',
      'Aircraft',
      function () {
        return [];
      }
    ]
  };

  angular.module('gva').controller('AircraftsSearchCtrl', AircraftsSearchCtrl);
}(angular, _));
