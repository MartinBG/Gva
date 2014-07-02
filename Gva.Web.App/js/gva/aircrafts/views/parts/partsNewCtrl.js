/*global angular*/
(function (angular) {
  'use strict';

  function PartsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftParts,
    aircraftPart
  ) {
    $scope.save = function () {
      return $scope.newPartForm.$validate()
        .then(function () {
          if ($scope.newPartForm.$valid) {
            return AircraftParts
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
    'AircraftParts',
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