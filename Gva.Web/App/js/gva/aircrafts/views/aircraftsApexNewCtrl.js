/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsApexNewCtrl($scope, $state, AircraftApex, aircraft) {
    $scope.aircraft = aircraft;

    $scope.save = function () {
      $scope.newAircraftForm.$validate()
      .then(function () {
        if ($scope.newAircraftForm.$valid) {
          return AircraftApex.save($scope.aircraft).$promise
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

  AircraftsApexNewCtrl.$inject = ['$scope', '$state', 'AircraftApex', 'aircraft'];

  AircraftsApexNewCtrl.$resolve = {
    aircraft: function () {
      return {
        aircraftDataApex: {}
      };
    }
  };

  angular.module('gva').controller('AircraftsApexNewCtrl', AircraftsApexNewCtrl);
}(angular));
