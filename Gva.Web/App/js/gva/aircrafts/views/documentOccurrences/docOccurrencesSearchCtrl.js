/*global angular*/
(function (angular) {
  'use strict';

  function DocOccurrencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOccurrence,
    aircraftDocumentOccurrences) {

    $scope.aircraftDocumentOccurrences = aircraftDocumentOccurrences;

    $scope.search = function () {
      $state.go('root.aircrafts.view.occurrences.search', {
      });
    };

    $scope.newOccurrence = function () {
      return $state.go('root.aircrafts.view.occurrences.new');
    };

    $scope.editOccurrence = function (occurrence) {
      return $state.go('root.aircrafts.view.occurrences.edit', {
        id: $stateParams.id,
        ind: occurrence.partIndex
      });
    };
  }

  DocOccurrencesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOccurrence',
    'aircraftDocumentOccurrences'
  ];

  DocOccurrencesSearchCtrl.$resolve = {
    aircraftDocumentOccurrences: [
      '$stateParams',
      'AircraftDocumentOccurrence',
      function ($stateParams, AircraftDocumentOccurrence) {
        return AircraftDocumentOccurrence.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesSearchCtrl', DocOccurrencesSearchCtrl);
}(angular));
