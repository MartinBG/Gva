/*global angular*/
(function (angular) {
  'use strict';

  function PartsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftPart,
    aircraftPart
  ) {

    $scope.aircraftPart = aircraftPart;

    $scope.save = function () {
      return $scope.aircraftPartForm.$validate()
        .then(function () {
          if ($scope.aircraftPartForm.$valid) {
            return AircraftPart
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftPart)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.parts.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.parts.search');
    };
  }

  PartsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftPart',
    'aircraftPart'
  ];

  PartsEditCtrl.$resolve = {
    aircraftPart: [
      '$stateParams',
      'AircraftPart',
      function ($stateParams, AircraftPart) {
        return AircraftPart.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PartsEditCtrl', PartsEditCtrl);
}(angular));