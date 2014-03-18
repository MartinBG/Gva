/*global angular*/
(function (angular) {
  'use strict';

  function MaintenancesNewCtrl(
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
    'AircraftMaintenance',
    'aircraftMaintenance'
  ];

  MaintenancesNewCtrl.$resolve = {
    aircraftMaintenance: function () {
      return {};
    }
  };

  angular.module('gva').controller('MaintenancesNewCtrl', MaintenancesNewCtrl);
}(angular));