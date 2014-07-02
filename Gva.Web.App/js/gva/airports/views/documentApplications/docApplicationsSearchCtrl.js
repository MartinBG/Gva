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


    $scope.editApplication = function (application) {
      return $state.go('root.airports.view.applications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.newApplication = function () {
      return $state.go('root.airports.view.applications.new');
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
        return AirportDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportApplicationsSearchCtrl', AirportApplicationsSearchCtrl);
}(angular));
