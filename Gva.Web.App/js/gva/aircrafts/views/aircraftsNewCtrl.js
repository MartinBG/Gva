/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsNewCtrl($scope, $state, Aircrafts, aircraft) {
    $scope.aircraft = aircraft;

    if ($state.payload) {
      if ($state.payload.aircraftModel) {
        $scope.aircraft.aircraftData.aircraftProducer =
          $state.payload.aircraftModel.textContent.aircraftProducer;
        $scope.aircraft.aircraftData.airCategory =
          $state.payload.aircraftModel.textContent.airCategory;
        $scope.aircraft.aircraftData.model = $state.payload.aircraftModel.name;
        $scope.aircraft.aircraftData.modelAlt = $state.payload.aircraftModel.nameAlt;
      } else {
        $scope.aircraft.aircraftData.aircraftProducer = $state.payload.aircraftProducer;
        $scope.aircraft.aircraftData.airCategory = $state.payload.airCategory;
      }
    }
    $scope.save = function () {
      return $scope.newAircraftForm.$validate()
      .then(function () {
        if ($scope.newAircraftForm.$valid) {
          return Aircrafts.save($scope.aircraft).$promise
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

  AircraftsNewCtrl.$inject = ['$scope', '$state', 'Aircrafts', 'aircraft'];

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
