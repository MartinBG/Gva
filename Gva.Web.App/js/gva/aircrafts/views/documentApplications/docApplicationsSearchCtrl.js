/*global angular*/
(function (angular) {
  'use strict';

  function AircraftApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplications,
    aircraftDocumentApplications
  ) {
    $scope.aircraftDocumentApplications = aircraftDocumentApplications;

    $scope.editApplication = function (application) {
      return $state.go('root.aircrafts.view.applications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.newApplication = function () {
      return $state.go('root.aircrafts.view.applications.new');
    };
  }

  AircraftApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentApplications',
    'aircraftDocumentApplications'
  ];

  AircraftApplicationsSearchCtrl.$resolve = {
    aircraftDocumentApplications: [
      '$stateParams',
      'AircraftDocumentApplications',
      function ($stateParams, AircraftDocumentApplications) {
        return AircraftDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('AircraftApplicationsSearchCtrl', AircraftApplicationsSearchCtrl);
}(angular));
