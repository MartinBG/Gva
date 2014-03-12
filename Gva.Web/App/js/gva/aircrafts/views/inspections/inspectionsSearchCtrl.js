/*global angular*/
(function (angular) {
  'use strict';

  function InspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspection,
    aircraftInspections) {

    $scope.aircraftInspections = aircraftInspections;

    $scope.search = function () {
      $state.go('root.aircrafts.view.inspections.search', {
      });
    };

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

  InspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspections'
  ];

  InspectionsSearchCtrl.$resolve = {
    aircraftInspections: [
      '$stateParams',
      'AircraftInspection',
      function ($stateParams, AircraftInspection) {
        return AircraftInspection.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('InspectionsSearchCtrl', InspectionsSearchCtrl);
}(angular));
