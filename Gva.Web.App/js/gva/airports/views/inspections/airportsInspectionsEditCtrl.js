﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspections,
    airportInspection,
    scMessage
  ) {
    var originalInspection = _.cloneDeep(airportInspection);
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.airportInspection = airportInspection;
    $scope.lotId = $stateParams.lotId;

    $scope.editMode = null;
    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportInspection = _.cloneDeep(originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return AirportInspections
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportInspection)
            .$promise
            .then(function () {
              return $state.go('root.airports.view.inspections.search');
            });
        }
      });
    };

    $scope.deleteInspection = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AirportInspections.remove({
            id: $stateParams.id,
            ind: $stateParams.ind
          }).$promise.then(function () {
            return $state.go('root.airports.view.inspections.search');
          });
        }
      });
    };
  }

  AirportsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportInspections',
    'airportInspection',
    'scMessage'
  ];

  AirportsInspectionsEditCtrl.$resolve = {
    airportInspection: [
      '$stateParams',
      'AirportInspections',
      function ($stateParams, AirportInspections) {
        return AirportInspections.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsInspectionsEditCtrl', AirportsInspectionsEditCtrl);
}(angular, _));
