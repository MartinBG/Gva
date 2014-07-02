/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    airportInspections) {

    $scope.airportInspections = airportInspections;

    $scope.newInspection = function () {
      return $state.go('root.airports.view.inspections.new');
    };

    $scope.editInspection = function (inspection) {
      return $state.go('root.airports.view.inspections.edit', {
        id: $stateParams.id,
        ind: inspection.partIndex
      });
    };

  }

  AirportsInspectionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'airportInspections'
  ];

  AirportsInspectionsSearchCtrl.$resolve = {
    airportInspections: [
      '$stateParams',
      'AirportInspections',
      function ($stateParams, AirportInspections) {
        return AirportInspections.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AirportsInspectionsSearchCtrl', AirportsInspectionsSearchCtrl);
}(angular));
