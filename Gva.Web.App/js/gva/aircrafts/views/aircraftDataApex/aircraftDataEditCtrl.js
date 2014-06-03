﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftDataApexEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDataApex,
    aircraftData
  ) {
    var originalAircraftData = _.cloneDeep(aircraftData);

    $scope.aircraftData = aircraftData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftData = _.cloneDeep(originalAircraftData);
    };

    $scope.save = function () {
      return $scope.editAircraftForm.$validate()
      .then(function () {
        if ($scope.editAircraftForm.$valid) {
          return AircraftDataApex
          .save({ id: $stateParams.id }, $scope.aircraftData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.aircrafts.view', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view');
    };
  }

  AircraftDataApexEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDataApex',
    'aircraftData'
  ];

  AircraftDataApexEditCtrl.$resolve = {
    aircraftData: [
      '$stateParams',
      'AircraftDataApex',
      function ($stateParams, AircraftDataApex) {
        return AircraftDataApex.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftDataApexEditCtrl', AircraftDataApexEditCtrl);
}(angular));