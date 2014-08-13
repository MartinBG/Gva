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
    $scope.lotId = $stateParams.id;

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
      '$stateParams',
      'AircraftDocumentOccurrences',
      function ($stateParams, AircraftDocumentOccurrences) {
        return AircraftDocumentOccurrences.newDocumentOccurrence({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesNewCtrl', DocOccurrencesNewCtrl);
}(angular));
