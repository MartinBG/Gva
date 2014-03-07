/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsNewCtrl($scope, $state, Aircraft, aircraft) {
    $scope.aircraft = aircraft;

    $scope.save = function () {
      $scope.newAircraftForm.$validate()
      .then(function () {
        if ($scope.newAircraftForm.$valid) {
          return Aircraft.save($scope.aircraft).$promise
            .then(function () {
              return $state.go('root.aircrafts.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.search');
    };
  }

  AircraftsNewCtrl.$inject = ['$scope', '$state', 'Aircraft', 'aircraft'];

  AircraftsNewCtrl.$resolve = {
    aircraft: function () {
      return {
        aircraftData: {}
      };
    }
  };

  angular.module('gva').controller('AircraftsNewCtrl', AircraftsNewCtrl);
}(angular));
