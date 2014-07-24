/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectAirportCtrl($scope, namedModal) {
    $scope.chooseAirport = function () {
      var modalInstance = namedModal.open('chooseAirport');

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };

    $scope.newAirport = function () {
      var modalInstance = namedModal.open('newAirport');

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectAirportCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('AppSelectAirportCtrl', AppSelectAirportCtrl);
}(angular));
