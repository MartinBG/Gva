﻿/*global angular,_*/
(function (angular,_) {
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
        return AircraftDocumentOccurrences.query($stateParams)
          .$promise.then(function (occurrrences) {
            return _.map(occurrrences, function (occ) {
              occ.part.time = {
                hours: Math.floor(occ.part.localTime / 3600000) % 60,
                minutes: (occ.part.localTime / 60000) % 60
              };

              return occ;
            });
          });
      }
    ]
  };

  angular.module('gva').controller('DocOccurrencesSearchCtrl', DocOccurrencesSearchCtrl);
}(angular,_));
