/*global angular*/
(function (angular) {
  'use strict';

  function AirportsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Airport,
    airport
  ) {
    $scope.airport = airport;

    $scope.edit = function () {
      return $state.go('root.airports.view.edit');
    };
  }

  AirportsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Airport',
    'airport'
  ];

  AirportsViewCtrl.$resolve = {
    airport: [
      '$stateParams',
      'Airport',
      function ($stateParams, Airport) {
        return Airport.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsViewCtrl', AirportsViewCtrl);
}(angular));
