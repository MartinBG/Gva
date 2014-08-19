/*global angular*/
(function (angular) {
  'use strict';

  function MaintenancesNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftMaintenances,
    aircraftMaintenance) {
    $scope.aircraftMaintenance = aircraftMaintenance;

    $scope.save = function () {
      return $scope.newAircraftMaintenanceForm.$validate()
      .then(function () {
        if ($scope.newAircraftMaintenanceForm.$valid) {
          return AircraftMaintenances
            .save({ id: $stateParams.id }, $scope.aircraftMaintenance)
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

  MaintenancesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftMaintenances',
    'aircraftMaintenance'
  ];

  MaintenancesNewCtrl.$resolve = {
    aircraftMaintenance: [
      '$stateParams',
      'AircraftMaintenances',
      function ($stateParams, AircraftMaintenances) {
        return AircraftMaintenances.newMaintenance({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('MaintenancesNewCtrl', MaintenancesNewCtrl);
}(angular));