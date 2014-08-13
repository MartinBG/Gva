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
    $scope.lotId = $stateParams.id;

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
    aircraftDocumentApplication: [
      '$stateParams',
      'AircraftDocumentApplications',
      function ($stateParams, AircraftDocumentApplications) {
        return AircraftDocumentApplications.newDocumentApplication({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftApplicationsNewCtrl', AircraftApplicationsNewCtrl);
}(angular));
