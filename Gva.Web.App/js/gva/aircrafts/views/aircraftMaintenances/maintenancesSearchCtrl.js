/*global angular*/
(function (angular) {
  'use strict';

  function MaintenancesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftMaintenance,
    aircraftMaintenances) {

    $scope.aircraftMaintenances = aircraftMaintenances;

    $scope.search = function () {
      $state.go('root.aircrafts.view.maintenances.search', {
      });
    };

    $scope.newMaintenance = function () {
      return $state.go('root.aircrafts.view.maintenances.new');
    };

    $scope.editMaintenance = function (maintenance) {
      return $state.go('root.aircrafts.view.maintenances.edit', {
        id: $stateParams.id,
        ind: maintenance.partIndex
      });
    };
  }

  MaintenancesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftMaintenance',
    'aircraftMaintenances'
  ];

  MaintenancesSearchCtrl.$resolve = {
    aircraftMaintenances: [
      '$stateParams',
      'AircraftMaintenance',
      function ($stateParams, AircraftMaintenance) {
        return AircraftMaintenance.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('MaintenancesSearchCtrl', MaintenancesSearchCtrl);
}(angular));