/*global angular*/
(function (angular) {
  'use strict';

  function DocOccurrencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'aircraftDocumentOccurrences'
  ];

  DocOccurrencesSearchCtrl.$resolve = {
    aircraftDocumentOccurrences: [
      '$stateParams',
      'AircraftDocumentOccurrences',
      function ($stateParams, AircraftDocumentOccurrences) {
        return AircraftDocumentOccurrences.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesSearchCtrl', DocOccurrencesSearchCtrl);
}(angular));
