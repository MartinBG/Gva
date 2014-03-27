/*global angular*/
(function (angular) {
  'use strict';

  function AircraftApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplication,
    aircraftDocumentApplication) {

    $scope.aircraftDocumentApplication = aircraftDocumentApplication;

    $scope.save = function () {
      $scope.editDocumentApplicationForm.$validate()
      .then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return AircraftDocumentApplication
            .save(
            { id: $stateParams.id, ind: $stateParams.ind },
            $scope.aircraftDocumentApplication)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.applications.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.applications.search');
    };
  }

  AircraftApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentApplication',
    'aircraftDocumentApplication'
  ];

  AircraftApplicationsEditCtrl.$resolve = {
    aircraftDocumentApplication: [
      '$stateParams',
      'AircraftDocumentApplication',
      function ($stateParams, AircraftDocumentApplication) {
        return AircraftDocumentApplication.get($stateParams).$promise
            .then(function (application) {
          application.files = {
            hideApplications: true,
            files: application.files
          };

          return application;
        });
      }
    ]
  };

  angular.module('gva').controller('AircraftApplicationsEditCtrl', AircraftApplicationsEditCtrl);
}(angular));