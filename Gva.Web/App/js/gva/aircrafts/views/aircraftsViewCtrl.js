/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Aircraft,
    aircraft
  ) {
    $scope.aircraft = aircraft;

    $scope.edit = function () {
      return $state.go('root.aircrafts.view.edit');
    };
  }

  AircraftsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircraft',
    'aircraft'
  ];

  AircraftsViewCtrl.$resolve = {
    aircraft: [
      '$stateParams',
      'Aircraft',
      function ($stateParams, Aircraft) {
        return Aircraft.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsViewCtrl', AircraftsViewCtrl);
}(angular));
