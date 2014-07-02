/*global angular*/
(function (angular) {
  'use strict';

  function DocOccurrencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOccurrences,
    aircraftDocumentOccurrence) {
    $scope.aircraftDocumentOccurrence = aircraftDocumentOccurrence;

    $scope.save = function () {
      return $scope.newAircraftDocumentOccurrenceForm.$validate()
      .then(function () {
        if ($scope.newAircraftDocumentOccurrenceForm.$valid) {
          return AircraftDocumentOccurrences
            .save({ id: $stateParams.id }, $scope.aircraftDocumentOccurrence)
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

  DocOccurrencesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOccurrences',
    'aircraftDocumentOccurrence'
  ];

  DocOccurrencesNewCtrl.$resolve = {
    aircraftDocumentOccurrence: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesNewCtrl', DocOccurrencesNewCtrl);
}(angular));
