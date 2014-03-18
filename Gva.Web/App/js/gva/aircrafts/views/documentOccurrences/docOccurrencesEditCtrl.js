/*global angular*/
(function (angular) {
  'use strict';

  function DocOccurrencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOccurrence,
    aircraftDocumentOccurrence) {
    $scope.aircraftDocumentOccurrence = aircraftDocumentOccurrence;

    $scope.save = function () {
      return $scope.aircraftDocumentOccurrenceForm.$validate()
      .then(function () {
        if ($scope.aircraftDocumentOccurrenceForm.$valid) {
          return AircraftDocumentOccurrence
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOccurrence)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.occurrences.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.occurrences.search');
    };
  }

  DocOccurrencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOccurrence',
    'aircraftDocumentOccurrence'
  ];

  DocOccurrencesEditCtrl.$resolve = {
    aircraftDocumentOccurrence: [
      '$stateParams',
      'AircraftDocumentOccurrence',
      function ($stateParams, AircraftDocumentOccurrence) {
        return AircraftDocumentOccurrence.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesEditCtrl', DocOccurrencesEditCtrl);
}(angular));
