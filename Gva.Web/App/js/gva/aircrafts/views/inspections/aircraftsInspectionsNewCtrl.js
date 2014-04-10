/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspection,
    aircraftInspection) {
    $scope.aircraftInspection = aircraftInspection;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return AircraftInspection
            .save({ id: $stateParams.id }, $scope.aircraftInspection)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.inspections.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.inspections.search');
    };
  }

  AircraftsInspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspection'
  ];

  AircraftsInspectionsNewCtrl.$resolve = {
    aircraftInspection: [
      function () {
        return {
          part: {}
        };
      }
    ]
  };

  angular.module('gva').controller('AircraftsInspectionsNewCtrl', AircraftsInspectionsNewCtrl);
}(angular));
