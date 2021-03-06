﻿/*global angular, _*/
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
      'Airports',
      function ($stateParams, Airports) {
        return Airports.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsSearchCtrl', AirportsSearchCtrl);
}(angular, _));
