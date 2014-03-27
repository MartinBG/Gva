/*global angular*/
(function (angular) {
  'use strict';

  function AirportApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentApplication,
    airportDocumentApplication
  ) {

    $scope.airportDocumentApplication = airportDocumentApplication;

    $scope.save = function () {
      $scope.newDocumentApplicationForm.$validate()
         .then(function () {
            if ($scope.newDocumentApplicationForm.$valid) {
              return AirportDocumentApplication
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
    'AirportDocumentApplication',
    'airportDocumentApplication'
  ];
  AirportApplicationsNewCtrl.$resolve = {
    airportDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('AirportApplicationsNewCtrl', AirportApplicationsNewCtrl);
}(angular));
