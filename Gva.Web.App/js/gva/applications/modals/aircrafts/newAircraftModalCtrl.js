/*global angular*/
(function (angular) {
  'use strict';

  function NewAircraftModalCtrl(
    $scope,
    $modalInstance,
    Aircrafts
  ) {
    $scope.form = {};
    $scope.aircraft = {};

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
    'Aircrafts'
  ];

  angular.module('gva').controller('NewAircraftModalCtrl', NewAircraftModalCtrl);
}(angular));
