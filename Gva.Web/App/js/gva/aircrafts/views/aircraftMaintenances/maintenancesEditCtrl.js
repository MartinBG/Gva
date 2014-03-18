/*global angular*/
(function (angular) {
  'use strict';

  function MaintenancesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftMaintenance,
    aircraftMaintenance) {
    $scope.aircraftMaintenance = aircraftMaintenance;

    $scope.save = function () {
      return $scope.aircraftMaintenanceForm.$validate()
      .then(function () {
        if ($scope.aircraftMaintenanceForm.$valid) {
          return AircraftMaintenance
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftMaintenance)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.maintenances.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.maintenances.search');
    };
  }

  MaintenancesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftMaintenance',
    'aircraftMaintenance'
  ];

  MaintenancesEditCtrl.$resolve = {
    aircraftMaintenance: [
      '$stateParams',
      'AircraftMaintenance',
      function ($stateParams, AircraftMaintenance) {
        return AircraftMaintenance.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('MaintenancesEditCtrl', MaintenancesEditCtrl);
}(angular));