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
      'ApplicationNoms',
      function ResolveApplication($stateParams, ApplicationNoms) {
        if ($stateParams.appId) {
          return ApplicationNoms
            .get({ lotId: $stateParams.id, appId: $stateParams.appId })
            .$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('AirportsViewCtrl', AirportsViewCtrl);
}(angular));
