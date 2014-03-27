/*global angular*/
(function (angular) {
  'use strict';

  function AircraftApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentApplication,
    aircraftDocumentApplications
  ) {
    $scope.aircraftDocumentApplications = aircraftDocumentApplications;


    $scope.editApplication = function (application) {
      return $state.go('root.aircrafts.view.applications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.deleteApplication = function (application) {
      return AircraftDocumentApplication.remove({ id: $stateParams.id, ind: application.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftDocumentApplication',
    'aircraftDocumentApplications'
  ];

  AircraftApplicationsSearchCtrl.$resolve = {
    aircraftDocumentApplications: [
      '$stateParams',
      'AircraftDocumentApplication',
      function ($stateParams, AircraftDocumentApplication) {
        return AircraftDocumentApplication.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('AircraftApplicationsSearchCtrl', AircraftApplicationsSearchCtrl);
}(angular));
