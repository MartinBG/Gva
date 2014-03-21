/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftData,
    aircraftData
  ) {
    $scope.aircraftData = aircraftData;

    $scope.save = function () {
      return $scope.aircraftDataForm.$validate()
      .then(function () {
        if ($scope.aircraftDataForm.$valid) {
          return AircraftData
          .save({ id: $stateParams.id }, $scope.aircraftData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.aircrafts.view', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view');
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
