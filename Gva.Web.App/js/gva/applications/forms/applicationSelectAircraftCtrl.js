/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectAircraftCtrl($scope, scModal) {
    $scope.chooseAircraft = function () {
      var modalInstance = scModal.open('chooseAircraft');

      modalInstance.result.then(function (aircraftId) {
        $scope.model.lot.id = aircraftId;
      });

      return modalInstance.opened;
    };

    $scope.newAircraft = function () {
      var modalInstance = scModal.open('newAircraft');

      modalInstance.result.then(function (aircraftId) {
        $scope.model.lot.id = aircraftId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectAircraftCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppSelectAircraftCtrl', AppSelectAircraftCtrl);
}(angular));
