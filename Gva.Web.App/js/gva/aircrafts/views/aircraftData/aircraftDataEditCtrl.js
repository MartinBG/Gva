/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftsData,
    aircraftData
  ) {
    var originalAircraftData = _.cloneDeep(aircraftData);

    $scope.aircraftData = aircraftData;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftData = _.cloneDeep(originalAircraftData);
    };

    $scope.save = function () {
      return $scope.editAircraftForm.$validate()
      .then(function () {
        if ($scope.editAircraftForm.$valid) {
          return AircraftsData
          .save({ id: $stateParams.id }, $scope.aircraftData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.aircrafts.view', $stateParams, { reload: true });
          });
        }
      });
    };
  }

  AircraftDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftsData',
    'aircraftData'
  ];

  AircraftDataEditCtrl.$resolve = {
    aircraftData: [
      '$stateParams',
      'AircraftsData',
      function ($stateParams, AircraftsData) {
        return AircraftsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftDataEditCtrl', AircraftDataEditCtrl);
}(angular));
