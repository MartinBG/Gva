/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspections,
    aircraftInspection
  ) {
    $scope.aircraftInspection = aircraftInspection;
    $scope.lotId = $stateParams.lotId;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return AircraftInspections
            .save({ id: $stateParams.id }, $scope.aircraftInspection)
            .$promise
            .then(function (inspection) {
              return $state.go('root.aircrafts.view.inspections.edit', { 
                  id: $stateParams.id,
                  ind: inspection.partIndex
                });
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
    'AircraftInspections',
    'aircraftInspection'
  ];

  AircraftsInspectionsNewCtrl.$resolve = {
    aircraftInspection: [
      '$stateParams',
      'AircraftInspections',
      function ($stateParams, AircraftInspections) {
        return AircraftInspections.newInspection({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsInspectionsNewCtrl', AircraftsInspectionsNewCtrl);
}(angular));
