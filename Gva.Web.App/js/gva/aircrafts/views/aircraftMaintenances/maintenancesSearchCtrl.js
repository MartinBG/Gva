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