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

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', {
        id: appId,
        set: $stateParams.set
      });
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
      'AircraftApplications',
      function ResolveApplication($stateParams, AircraftApplications) {
        if (!!$stateParams.appId) {
          return AircraftApplications.get($stateParams).$promise
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
