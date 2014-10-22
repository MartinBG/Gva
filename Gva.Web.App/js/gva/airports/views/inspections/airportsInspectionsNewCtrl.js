/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspections,
    airportInspection
  ) {
    $scope.airportInspection = airportInspection;
    $scope.lotId = $stateParams.lotId;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return AirportInspections
            .save({ id: $stateParams.id }, $scope.airportInspection)
            .$promise
            .then(function (inspection) {
              return $state.go('root.airports.view.inspections.edit', { 
                  id: $stateParams.id,
                  ind: inspection.partIndex
                });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.view.inspections.search');
    };
  }

  AirportsInspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportInspections',
    'airportInspection'
  ];

  AirportsInspectionsNewCtrl.$resolve = {
    airportInspection: [
      '$stateParams',
      'AirportInspections',
      function ($stateParams, AirportInspections) {
        return AirportInspections.newInspection({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsInspectionsNewCtrl', AirportsInspectionsNewCtrl);
}(angular));
