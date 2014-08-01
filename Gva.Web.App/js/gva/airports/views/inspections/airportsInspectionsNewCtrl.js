/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspections,
    airportInspection) {
    $scope.airportInspection = airportInspection;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return AirportInspections
            .save({ id: $stateParams.id }, $scope.airportInspection)
            .$promise
            .then(function () {
              return $state.go('root.airports.view.inspections.search');
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
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              examiners: [],
              auditDetails: [],
              disparities: []
            },
            applications: [application]
          };
        }
        else {
          return {
            part: {
              examiners: [],
              auditDetails: [],
              disparities: []
            },
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('AirportsInspectionsNewCtrl', AirportsInspectionsNewCtrl);
}(angular));
