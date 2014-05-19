/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspection,
    aircraftInspections) {

    $scope.aircraftInspections = aircraftInspections;

    $scope.newInspection = function () {
      return $state.go('root.aircrafts.view.inspections.new');
    };

    $scope.editInspection = function (inspection) {
      return $state.go('root.aircrafts.view.inspections.edit', {
        id: $stateParams.id,
        ind: inspection.partIndex
      });
    };

  }

  AircraftsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspections'
  ];

  AircraftsInspectionsSearchCtrl.$resolve = {
    aircraftInspections: [
      '$stateParams',
      'AircraftInspection',
      function ($stateParams, AircraftInspection) {
        return AircraftInspection.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AircraftsInspectionsSearchCtrl', AircraftsInspectionsSearchCtrl);
}(angular));
