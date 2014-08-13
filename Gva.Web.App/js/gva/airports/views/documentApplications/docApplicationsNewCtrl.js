/*global angular*/
(function (angular) {
  'use strict';

  function AirportApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentApplications,
    airportDocumentApplication
  ) {
    $scope.airportDocumentApplication = airportDocumentApplication;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.newDocumentApplicationForm.$valid) {
            return AirportDocumentApplications
              .save({ id: $stateParams.id }, $scope.airportDocumentApplication).$promise
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

  AirportApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentApplications',
    'airportDocumentApplication'
  ];
  AirportApplicationsNewCtrl.$resolve = {
    airportDocumentApplication: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('AirportApplicationsNewCtrl', AirportApplicationsNewCtrl);
}(angular));
