/*global angular*/
(function (angular) {
  'use strict';

  function ChooseAirportModalCtrl(
    $scope,
    $modalInstance,
    Airports,
    airports
  ) {
    $scope.airports = airports;

    $scope.filters = {
      manSN: null,
      icao: null
    };

    $scope.search = function () {
      return Airports.query($scope.filters).$promise.then(function (airports) {
        $scope.airports = airports;
      });
    };

    $scope.selectAirport = function (airport) {
      return $modalInstance.close(airport.id);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseAirportModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Airports',
    'airports'
  ];

  angular.module('gva').controller('ChooseAirportModalCtrl', ChooseAirportModalCtrl);
}(angular));
