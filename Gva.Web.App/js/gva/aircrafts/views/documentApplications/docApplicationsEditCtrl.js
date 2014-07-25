/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplications,
    aircraftDocumentApplication,
    scMessage) {
    var originalApplication = _.cloneDeep(aircraftDocumentApplication);

    $scope.aircraftDocumentApplication = aircraftDocumentApplication;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

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
            return AircraftDocumentApplications
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
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftDocumentApplications.remove({
            id: $stateParams.id,
            ind: aircraftDocumentApplication.partIndex
          }).$promise.then(function () {
            return $state.go('root.aircrafts.view.applications.search', {
              appId: null
            }, { reload: true });
          });
        }
      });
    };
  }

  AircraftApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentApplications',
    'aircraftDocumentApplication',
    'scMessage'
  ];

  AircraftApplicationsEditCtrl.$resolve = {
    aircraftDocumentApplication: [
      '$stateParams',
      'AircraftDocumentApplications',
      function ($stateParams, AircraftDocumentApplications) {
        return AircraftDocumentApplications.get($stateParams).$promise
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
