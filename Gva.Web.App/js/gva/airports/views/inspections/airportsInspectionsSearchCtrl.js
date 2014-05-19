/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspection,
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
    'AirportInspection',
    'airportInspections'
  ];

  AirportsInspectionsSearchCtrl.$resolve = {
    airportInspections: [
      '$stateParams',
      'AirportInspection',
      function ($stateParams, AirportInspection) {
        return AirportInspection.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AirportsInspectionsSearchCtrl', AirportsInspectionsSearchCtrl);
}(angular));
