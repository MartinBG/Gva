/*global angular*/
(function (angular) {
  'use strict';

  function MaintenancesSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'aircraftMaintenances'
  ];

  MaintenancesSearchCtrl.$resolve = {
    aircraftMaintenances: [
      '$stateParams',
      'AircraftMaintenances',
      function ($stateParams, AircraftMaintenances) {
        return AircraftMaintenances.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('MaintenancesSearchCtrl', MaintenancesSearchCtrl);
}(angular));