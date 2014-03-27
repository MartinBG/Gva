/*global angular*/
(function (angular) {
  'use strict';

  function AirportDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportData,
    airportData
  ) {
    $scope.airportData = airportData;

    $scope.save = function () {
      return $scope.airportDataForm.$validate()
      .then(function () {
        if ($scope.airportDataForm.$valid) {
          return AirportData
          .save({ id: $stateParams.id }, $scope.airportData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.airports.view', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.view');
    };
  }

  AirportDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportData',
    'airportData'
  ];

  AirportDataEditCtrl.$resolve = {
    airportData: [
      '$stateParams',
      'AirportData',
      function ($stateParams, AirportData) {
        return AirportData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportDataEditCtrl', AirportDataEditCtrl);
}(angular));
