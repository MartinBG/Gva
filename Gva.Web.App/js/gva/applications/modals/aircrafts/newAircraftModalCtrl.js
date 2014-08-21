/*global angular*/
(function (angular) {
  'use strict';

  function NewAircraftModalCtrl(
    $scope,
    $modalInstance,
    Aircrafts,
    aircraft
  ) {
    $scope.form = {};
    $scope.aircraft = aircraft;

    $scope.save = function () {
      return $scope.form.newAircraftForm.$validate().then(function () {
        if ($scope.form.newAircraftForm.$valid) {
          return Aircrafts.save($scope.aircraft).$promise.then(function (savedAircraft) {
            return $modalInstance.close(savedAircraft.id);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewAircraftModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Aircrafts',
    'aircraft'
  ];

  NewAircraftModalCtrl.$resolve = {
    aircraft: [
      'Aircrafts',
      function (Aircrafts) {
        return Aircrafts.newAircraft().$promise;
      }
    ]
  };

  angular.module('gva').controller('NewAircraftModalCtrl', NewAircraftModalCtrl);
}(angular));
