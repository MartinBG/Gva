/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataApexEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDataApex,
    aircraftData
  ) {
    $scope.aircraftData = aircraftData;

    $scope.save = function () {
      return $scope.aircraftDataForm.$validate()
      .then(function () {
        if ($scope.aircraftDataForm.$valid) {
          return AircraftDataApex
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

  AircraftDataApexEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDataApex',
    'aircraftData'
  ];

  AircraftDataApexEditCtrl.$resolve = {
    aircraftData: [
      '$stateParams',
      'AircraftDataApex',
      function ($stateParams, AircraftDataApex) {
        return AircraftDataApex.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftDataApexEditCtrl', AircraftDataApexEditCtrl);
}(angular));
