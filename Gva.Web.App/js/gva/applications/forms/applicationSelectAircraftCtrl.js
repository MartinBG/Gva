/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectAircraftCtrl($scope, namedModal) {
    $scope.chooseAircraft = function () {
      var modalInstance = namedModal.open('chooseAircraft');

      modalInstance.result.then(function (aircraftId) {
        $scope.model.lot.id = aircraftId;
      });

      return modalInstance.opened;
    };

    $scope.newAircraft = function () {
      var modalInstance = namedModal.open('newAircraft');

      modalInstance.result.then(function (aircraftId) {
        $scope.model.lot.id = aircraftId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectAircraftCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('AppSelectAircraftCtrl', AppSelectAircraftCtrl);
}(angular));
