/*global angular*/
(function (angular) {
  'use strict';

  function AircraftApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplications,
    aircraftDocumentApplication
  ) {

    $scope.aircraftDocumentApplication = aircraftDocumentApplication;

    $scope.save = function () {
      return $scope.newDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.newDocumentApplicationForm.$valid) {
            return AircraftDocumentApplications
              .save({ id: $stateParams.id }, $scope.aircraftDocumentApplication)
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

  AircraftApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentApplications',
    'aircraftDocumentApplication'
  ];
  AircraftApplicationsNewCtrl.$resolve = {
    aircraftDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('AircraftApplicationsNewCtrl', AircraftApplicationsNewCtrl);
}(angular));
