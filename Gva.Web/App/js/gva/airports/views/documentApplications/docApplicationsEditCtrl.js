/*global angular*/
(function (angular) {
  'use strict';

  function AirportApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentApplication,
    airportDocumentApplication) {

    $scope.airportDocumentApplication = airportDocumentApplication;

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return AirportDocumentApplication
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

    $scope.cancel = function () {
      return $state.go('root.airports.view.applications.search');
    };
  }

  AirportApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentApplication',
    'airportDocumentApplication'
  ];

  AirportApplicationsEditCtrl.$resolve = {
    airportDocumentApplication: [
      '$stateParams',
      'AirportDocumentApplication',
      function ($stateParams, AirportDocumentApplication) {
        return AirportDocumentApplication.get($stateParams).$promise
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
}(angular));