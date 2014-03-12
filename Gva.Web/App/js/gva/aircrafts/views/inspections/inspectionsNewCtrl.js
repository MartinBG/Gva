/*global angular*/
(function (angular) {
  'use strict';

  function InspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspection,
    aircraftInspection) {
    $scope.aircraftInspection = aircraftInspection;

    $scope.save = function () {
      $scope.aircraftInspectionForm.$validate()
      .then(function () {
        if ($scope.aircraftInspectionForm.$valid) {
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

  InspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspection'
  ];

  InspectionsNewCtrl.$resolve = {
    aircraftInspection: [
      function () {
        return {
          part: {
            examiners: [{ sortOrder: 1 }],
            auditDetails: [],
            disparities: [],
            disparityNumber: 0
          }
        };
      }
    ]
  };

  angular.module('gva').controller('InspectionsNewCtrl', InspectionsNewCtrl);
}(angular));
