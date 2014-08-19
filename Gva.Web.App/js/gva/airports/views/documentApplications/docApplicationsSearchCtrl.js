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
        return AirportDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportApplicationsSearchCtrl', AirportApplicationsSearchCtrl);
}(angular));
