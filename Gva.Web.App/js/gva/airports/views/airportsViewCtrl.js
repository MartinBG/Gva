/*global angular*/
(function (angular) {
  'use strict';

  function AirportsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Airport,
    airport,
    application
  ) {
    $scope.airport = airport;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.airports.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', {
        id: appId,
        filter: $stateParams.filter
      });
    };
  }

  AirportsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Airport',
    'airport',
    'application'
  ];

  AirportsViewCtrl.$resolve = {
    airport: [
      '$stateParams',
      'Airport',
      function ($stateParams, Airport) {
        return Airport.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'AirportApplication',
      function ResolveApplication($stateParams, AirportApplication) {
        if (!!$stateParams.appId) {
          return AirportApplication.get($stateParams).$promise
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
