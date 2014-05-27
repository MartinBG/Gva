/*global angular*/
(function (angular) {
  'use strict';

  function AircraftsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Aircraft,
    aircraft,
    application
  ) {
    $scope.aircraft = aircraft;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.aircrafts.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', {
        id: appId,
        filter: $stateParams.filter
      });
    };
  }

  AircraftsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircraft',
    'aircraft',
    'application'
  ];

  AircraftsViewCtrl.$resolve = {
    aircraft: [
      '$stateParams',
      'Aircraft',
      function ($stateParams, Aircraft) {
        return Aircraft.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'AircraftApplication',
      function ResolveApplication($stateParams, AircraftApplication) {
        if (!!$stateParams.appId) {
          return AircraftApplication.get($stateParams).$promise
            .then(function (result) {
              if (result.applicationId) {
                return result;
              }

              return null;
            });
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('AircraftsViewCtrl', AircraftsViewCtrl);
}(angular));
