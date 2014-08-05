/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectAirportCtrl($scope, scModal) {
    $scope.chooseAirport = function () {
      var modalInstance = scModal.open('chooseAirport');

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };

    $scope.newAirport = function () {
      var modalInstance = scModal.open('newAirport');

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectAirportCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppSelectAirportCtrl', AppSelectAirportCtrl);
}(angular));
