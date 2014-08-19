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
