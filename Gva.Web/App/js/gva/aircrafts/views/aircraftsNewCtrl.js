/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsNewCtrl($scope, $state, Aircraft, aircraft) {
    $scope.aircraft = aircraft;

    if ($state.payload) {
      if ($state.payload.aircraftModel) {
        $scope.aircraft.aircraftData.aircraftProducer =
          $state.payload.aircraftModel.aircraftProducer;
        $scope.aircraft.aircraftData.aircraftCategory =
          $state.payload.aircraftModel.aircraftCategory;
        $scope.aircraft.aircraftData.model = $state.payload.aircraftModel.name;
        $scope.aircraft.aircraftData.modelAlt = $state.payload.aircraftModel.nameAlt;
      } else {
        $scope.aircraft.aircraftData.aircraftProducer = $state.payload.aircraftProducer;
        $scope.aircraft.aircraftData.aircraftCategory = $state.payload.aircraftCategory;
      }
    }
    $scope.save = function () {
      return $scope.newAircraftForm.$validate()
      .then(function () {
        if ($scope.newAircraftForm.$valid) {
          return Aircraft.save($scope.aircraft).$promise
            .then(function (aircraft) {
              return $state.go('root.aircrafts.view.regsFM.newWizzard', {
                id: aircraft.id
              });
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
        aircraftData: {
          caseTypes: [
            {
              nomValueId: 3
            }
          ]
        }
      };
    }
  };

  angular.module('gva').controller('AircraftsNewCtrl', AircraftsNewCtrl);
}(angular));
