/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    airportInspections) {
    $scope.airportInspections = airportInspections;
  }

  AirportsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'airportInspections'
  ];

  AirportsInspectionsSearchCtrl.$resolve = {
    airportInspections: [
      '$stateParams',
      'AirportInspections',
      function ($stateParams, AirportInspections) {
        return AirportInspections.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AirportsInspectionsSearchCtrl', AirportsInspectionsSearchCtrl);
}(angular));
