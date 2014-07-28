/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentApplications,
    airportDocumentApplication,
    scMessage) {
    var originalApplication = _.cloneDeep(airportDocumentApplication);

    $scope.airportDocumentApplication = airportDocumentApplication;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportDocumentApplication = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return AirportDocumentApplications
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.airportDocumentApplication)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.applications.search');
              });
          }
        });
    };

    $scope.deleteApplication = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AirportDocumentApplications.remove({
            id: $stateParams.id,
            ind: airportDocumentApplication.partIndex
          }).$promise.then(function () {
            return $state.go('root.airports.view.applications.search', {
              appId: null
            }, { reload: true });
          });
        }
      });
    };
  }

  AirportApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentApplications',
    'airportDocumentApplication',
    'scMessage'
  ];

  AirportApplicationsEditCtrl.$resolve = {
    airportDocumentApplication: [
      '$stateParams',
      'AirportDocumentApplications',
      function ($stateParams, AirportDocumentApplications) {
        return AirportDocumentApplications.get($stateParams).$promise
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

  angular.module('gva').controller('AirportApplicationsEditCtrl', AirportApplicationsEditCtrl);
}(angular, _));
