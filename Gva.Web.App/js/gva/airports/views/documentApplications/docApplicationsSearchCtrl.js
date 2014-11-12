/*global angular*/
(function (angular) {
  'use strict';

  function AirportApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    airportDocumentApplications
  ) {
    $scope.airportDocumentApplications = airportDocumentApplications;

    $scope.isDeclinedApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'declined';
      }

      return false;
    };

    $scope.isDoneApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'done';
      }

      return false;
    };

  }

  AirportApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'airportDocumentApplications'
  ];

  AirportApplicationsSearchCtrl.$resolve = {
    airportDocumentApplications: [
      '$stateParams',
      'AirportDocumentApplications',
      function ($stateParams, AirportDocumentApplications) {
        return AirportDocumentApplications.query({lotId: $stateParams.id}).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportApplicationsSearchCtrl', AirportApplicationsSearchCtrl);
}(angular));
