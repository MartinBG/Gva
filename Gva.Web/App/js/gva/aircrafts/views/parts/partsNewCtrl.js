/*global angular*/
(function (angular) {
  'use strict';

  function PartsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftPart,
    aircraftPart
  ) {
    $scope.save = function () {
      return $scope.aircraftPartForm.$validate()
        .then(function () {
          if ($scope.aircraftPartForm.$valid) {
            return AircraftPart
              .save({ id: $stateParams.id }, $scope.aircraftPart).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.parts.search');
              });
          }
        });
    };

    $scope.aircraftPart = aircraftPart;

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.parts.search');
    };
  }

  PartsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftPart',
    'aircraftPart'
  ];

  PartsNewCtrl.$resolve = {
    aircraftPart: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('PartsNewCtrl', PartsNewCtrl);
}(angular));