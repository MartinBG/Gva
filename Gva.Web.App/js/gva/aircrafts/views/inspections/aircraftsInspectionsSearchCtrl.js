/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspections,
    aircraftInspections) {
    $scope.aircraftInspections = aircraftInspections;
  }

  AircraftsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspections',
    'aircraftInspections'
  ];

  AircraftsInspectionsSearchCtrl.$resolve = {
    aircraftInspections: [
      '$stateParams',
      'AircraftInspections',
      function ($stateParams, AircraftInspections) {
        return AircraftInspections.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AircraftsInspectionsSearchCtrl', AircraftsInspectionsSearchCtrl);
}(angular));
