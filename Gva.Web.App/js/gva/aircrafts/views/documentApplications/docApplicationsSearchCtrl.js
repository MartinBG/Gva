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
    $scope.lotId = $stateParams.id;

    $scope.isDeclinedApp = function(item) {
      if (item.stage) {
        return item.stage.alias === 'declined';
      }

      return false;
    };

    $scope.isDoneApp = function(item) {
      if (item.stage) {
        return item.stage.alias === 'done';
      }

      return false;
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
