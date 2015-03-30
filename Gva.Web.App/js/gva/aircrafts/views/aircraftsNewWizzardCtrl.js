/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsNewWizzardCtrl($scope, $state) {
    $scope.model = {};

    $scope.forward = function () {
      return $scope.newAircraftForm.$validate()
        .then(function () {
          if ($scope.newAircraftForm.$valid) {
            return $state.go('root.aircrafts.new', {}, {}, $scope.model);
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.search');
    };
  }

  AircraftsNewWizzardCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('AircraftsNewWizzardCtrl', AircraftsNewWizzardCtrl);
}(angular));
