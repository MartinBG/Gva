/*global angular,_*/
(function (angular) {
  'use strict';

  function DocOccurrencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOccurrences,
    aircraftDocumentOccurrence) {
    var originalOccurrence = _.cloneDeep(aircraftDocumentOccurrence);

    $scope.aircraftDocumentOccurrence = aircraftDocumentOccurrence;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftDocumentOccurrence = _.cloneDeep(originalOccurrence);
    };

    $scope.save = function () {
      return $scope.editAircraftDocumentOccurrenceForm.$validate()
      .then(function () {
        if ($scope.editAircraftDocumentOccurrenceForm.$valid) {
          return AircraftDocumentOccurrences
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOccurrence)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.occurrences.search');
            });
        }
      });
    };

    $scope.deleteOccurrence = function () {
      return AircraftDocumentOccurrences
        .remove({ id: $stateParams.id, ind: aircraftDocumentOccurrence.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.occurrences.search');
        });
    };
  }

  DocOccurrencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOccurrences',
    'aircraftDocumentOccurrence'
  ];

  DocOccurrencesEditCtrl.$resolve = {
    aircraftDocumentOccurrence: [
      '$stateParams',
      'AircraftDocumentOccurrences',
      function ($stateParams, AircraftDocumentOccurrences) {
        return AircraftDocumentOccurrences.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesEditCtrl', DocOccurrencesEditCtrl);
}(angular));
