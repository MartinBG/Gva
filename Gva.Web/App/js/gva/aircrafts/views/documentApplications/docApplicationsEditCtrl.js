/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplication,
    aircraftDocumentApplication) {
    var originalApplication = _.cloneDeep(aircraftDocumentApplication);

    $scope.aircraftDocumentApplication = aircraftDocumentApplication;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftDocumentApplication = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return AircraftDocumentApplication
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.aircraftDocumentApplication)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.applications.search');
              });
          }
        });
    };

    $scope.deleteApplication = function () {
      return AircraftDocumentApplication.remove({
        id: $stateParams.id,
        ind: aircraftDocumentApplication.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.applications.search');
        });
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