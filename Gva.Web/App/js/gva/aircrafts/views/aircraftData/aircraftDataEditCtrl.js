/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftData,
    aircraftData
  ) {
    var originalAircraftData = _.cloneDeep(aircraftData);

    $scope.aircraftData = aircraftData;
    $scope.editMode = null;

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
          return AircraftData
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
    'AircraftData',
    'aircraftData'
  ];

  AircraftDataEditCtrl.$resolve = {
    aircraftData: [
      '$stateParams',
      'AircraftData',
      function ($stateParams, AircraftData) {
        return AircraftData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftDataEditCtrl', AircraftDataEditCtrl);
}(angular));
