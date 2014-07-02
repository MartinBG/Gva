/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportsData,
    airportData
  ) {
    var originalAirportData = _.cloneDeep(airportData);

    $scope.airportData = airportData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportData = _.cloneDeep(originalAirportData);
    };

    $scope.save = function () {
      return $scope.airportDataForm.$validate()
      .then(function () {
        if ($scope.airportDataForm.$valid) {
          return AirportsData
          .save({ id: $stateParams.id }, $scope.airportData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.airports.view', $stateParams, { reload: true });
          });
        }
      });
    };
  }

  AirportDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportsData',
    'airportData'
  ];

  AirportDataEditCtrl.$resolve = {
    airportData: [
      '$stateParams',
      'AirportsData',
      function ($stateParams, AirportsData) {
        return AirportsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportDataEditCtrl', AirportDataEditCtrl);
}(angular, _));
