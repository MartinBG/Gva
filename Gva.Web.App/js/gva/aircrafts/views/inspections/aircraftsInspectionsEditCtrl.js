﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspections,
    aircraftInspection,
    scMessage) {
    var originalInspection = _.cloneDeep(aircraftInspection);

    $scope.aircraftInspection = aircraftInspection;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftInspection = _.cloneDeep(originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return AircraftInspections
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftInspection)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.inspections.search');
            });
        }
      });
    };

    $scope.deleteInspection = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftInspections.remove({
            id: $stateParams.id,
            ind: aircraftInspection.partIndex
          }).$promise.then(function () {
            return $state.go('root.aircrafts.view.inspections.search');
          });
        }
      });
    };
  }

  AircraftsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspections',
    'aircraftInspection',
    'scMessage'
  ];

  AircraftsInspectionsEditCtrl.$resolve = {
    aircraftInspection: [
      '$stateParams',
      'AircraftInspections',
      function ($stateParams, AircraftInspections) {
        return AircraftInspections.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsInspectionsEditCtrl', AircraftsInspectionsEditCtrl);
}(angular));
