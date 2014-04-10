/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspection,
    airportInspection) {
    $scope.airportInspection = airportInspection;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return AirportInspection
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
    'AirportInspection',
    'airportInspection'
  ];

  AirportsInspectionsNewCtrl.$resolve = {
    airportInspection: [
      function () {
        return {
          part: {
            examiners: [{ sortOrder: 1 }],
            auditDetails: [],
            disparities: []
          }
        };
      }
    ]
  };

  angular.module('gva').controller('AirportsInspectionsNewCtrl', AirportsInspectionsNewCtrl);
}(angular));
