/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsViewCtrl(
    $scope,
    $state,
    $stateParams,
    aircraft,
    application
  ) {
    $scope.aircraft = aircraft;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.aircrafts.view.edit');
    };
  }

  AircraftsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'aircraft',
    'application'
  ];

  AircraftsViewCtrl.$resolve = {
    aircraft: [
      '$stateParams',
      'Aircrafts',
      function ($stateParams, Aircrafts) {
        return Aircrafts.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'ApplicationNoms',
      function ResolveApplication($stateParams, ApplicationNoms) {
        if ($stateParams.appId) {
          return ApplicationNoms
            .get({ lotId: $stateParams.id, appId: $stateParams.appId })
            .$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('AircraftsViewCtrl', AircraftsViewCtrl);
}(angular));
