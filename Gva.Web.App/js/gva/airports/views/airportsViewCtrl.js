/*global angular*/
(function (angular) {
  'use strict';

  function AirportsViewCtrl(
    $scope,
    $state,
    $stateParams,
    airport,
    application
  ) {
    $scope.airport = airport;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.airports.view.edit');
    };

    $scope.viewApplication = function (applicationId, partIndex) {
      return $state.go('root.applications.edit.data', {
        id: applicationId,
        set: $stateParams.set,
        lotId: $stateParams.id,
        ind: partIndex
      });
    };

    $scope.exitApplication = function () {
      delete $stateParams.appId;
      $state.transitionTo($state.current, $stateParams, { reload: true });
    };
  }

  AirportsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'airport',
    'application'
  ];

  AirportsViewCtrl.$resolve = {
    airport: [
      '$stateParams',
      'Airports',
      function ($stateParams, Airports) {
        return Airports.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'AirportApplications',
      function ResolveApplication($stateParams, AirportApplications) {
        if (!!$stateParams.appId) {
          return AirportApplications.get($stateParams).$promise
            .then(function (result) {
              if (result.applicationId) {
                return result;
              }

              return null;
            });
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('AirportsViewCtrl', AirportsViewCtrl);
}(angular));
