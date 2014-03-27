/*global angular*/
(function (angular) {
  'use strict';

  function AirportApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentApplication,
    airportDocumentApplications
  ) {
    $scope.airportDocumentApplications = airportDocumentApplications;


    $scope.editApplication = function (application) {
      return $state.go('root.airports.view.applications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.deleteApplication = function (application) {
      return AirportDocumentApplication.remove({ id: $stateParams.id, ind: application.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AirportDocumentApplication',
    'airportDocumentApplications'
  ];

  AirportApplicationsSearchCtrl.$resolve = {
    airportDocumentApplications: [
      '$stateParams',
      'AirportDocumentApplication',
      function ($stateParams, AirportDocumentApplication) {
        return AirportDocumentApplication.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportApplicationsSearchCtrl', AirportApplicationsSearchCtrl);
}(angular));
