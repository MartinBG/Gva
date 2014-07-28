/*global angular,_*/
(function (angular) {
  'use strict';

  function MaintenancesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftMaintenances,
    aircraftMaintenance,
    scMessage) {
    var originalMaintenance = _.cloneDeep(aircraftMaintenance);

    $scope.aircraftMaintenance = aircraftMaintenance;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftMaintenance = _.cloneDeep(originalMaintenance);
    };

    $scope.save = function () {
      return $scope.editAircraftMaintenanceForm.$validate()
      .then(function () {
        if ($scope.editAircraftMaintenanceForm.$valid) {
          return AircraftMaintenances
          .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftMaintenance)
          .$promise
          .then(function () {
            return $state.go('root.aircrafts.view.maintenances.search');
          });
        }
      });
    };

    $scope.deleteMaintenance = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftMaintenances
            .remove({ id: $stateParams.id, ind: aircraftMaintenance.partIndex })
            .$promise.then(function () {
              return $state.go('root.aircrafts.view.maintenances.search');
            });
          }
        });
    };
  }

  MaintenancesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftMaintenances',
    'aircraftMaintenance',
    'scMessage'
  ];

  MaintenancesEditCtrl.$resolve = {
    aircraftMaintenance: [
      '$stateParams',
      'AircraftMaintenances',
      function ($stateParams, AircraftMaintenances) {
        return AircraftMaintenances.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('MaintenancesEditCtrl', MaintenancesEditCtrl);
}(angular));
