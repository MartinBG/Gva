/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsNewWizzardCtrl(
    $scope,
    $state
  ) {

    $scope.model = {};


    $scope.forward = function () {
      return $scope.newAircraftForm.$validate()
        .then(function () {
          if ($scope.newAircraftForm.$valid) {
            return $state.go('root.aircrafts.new', {}, {}, $scope.model);
          }
        });
    };

    var oldModel = null;
    $scope.$watch('model.aircraftModel', function (newVal, oldVal) {
      if (newVal && (newVal !== oldVal)) {
        oldModel = $scope.model.aircraftModel;
        $scope.model.airCategory = $scope.model.aircraftModel.textContent.airCategory;
        $scope.model.aircraftProducer = $scope.model.aircraftModel.textContent.aircraftProducer;
        $scope.$evalAsync(function() {
          if (oldModel) {
            $scope.model.aircraftModel = oldModel;
            oldModel = null;
          }
        });
      }
    });

    $scope.$watch('model.airCategory', function (newVal, oldVal) {
      if (newVal !== oldVal && !newVal) {
        oldModel = $scope.model.aircraftModel;
        $scope.$evalAsync(function() {
          if (oldModel) {
            $scope.model.aircraftModel = oldModel;
            oldModel = null;
          }
        });
      }
    });

    $scope.$watch('model.aircraftProducer', function (newVal, oldVal) {
      if (newVal !== oldVal && !newVal) {
        oldModel = $scope.model.aircraftModel;
        $scope.$evalAsync(function() {
          if (oldModel) {
            $scope.model.aircraftModel = oldModel;
            oldModel = null;
          }
        });
      }
    });

    $scope.cancel = function () {
      return $state.go('root.aircrafts.search');
    };
  }

  AircraftsNewWizzardCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('AircraftsNewWizzardCtrl', AircraftsNewWizzardCtrl);
}(angular));
