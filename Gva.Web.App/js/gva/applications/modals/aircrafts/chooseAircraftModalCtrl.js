/*global angular*/
(function (angular) {
  'use strict';

  function ChooseAircraftModalCtrl(
    $scope,
    $modalInstance,
    Aircrafts,
    aircrafts
  ) {
    $scope.aircrafts = aircrafts;

    $scope.filters = {
      manSN: null,
      model: null,
      icao: null
    };

    $scope.search = function () {
      return Aircrafts.query($scope.filters).$promise.then(function (aircrafts) {
        $scope.aircrafts = aircrafts;
      });
    };

    $scope.selectAircraft = function (aircraft) {
      return $modalInstance.close(aircraft.id);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseAircraftModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Aircrafts',
    'aircrafts'
  ];

  ChooseAircraftModalCtrl.$resolve = {
    aircrafts: [
      'Aircrafts',
      function (Aircrafts) {
        return Aircrafts.query().$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseAircraftModalCtrl', ChooseAircraftModalCtrl);
}(angular));
