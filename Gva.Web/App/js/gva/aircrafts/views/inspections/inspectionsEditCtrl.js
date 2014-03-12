/*global angular*/
(function (angular) {
  'use strict';

  function InspectionsEditCtrl(
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
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftInspection)
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

  InspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspection'
  ];

  InspectionsEditCtrl.$resolve = {
    aircraftInspection: [
      '$stateParams',
      'AircraftInspection',
      function ($stateParams, AircraftInspection) {
        return AircraftInspection.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('InspectionsEditCtrl', InspectionsEditCtrl);
}(angular));
