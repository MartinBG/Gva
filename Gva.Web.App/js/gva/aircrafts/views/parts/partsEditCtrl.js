/*global angular,_*/
(function (angular) {
  'use strict';

  function PartsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftParts,
    aircraftPart,
    scMessage
  ) {
    var originalPart = _.cloneDeep(aircraftPart);

    $scope.aircraftPart = aircraftPart;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftPart = _.cloneDeep(originalPart);
    };

    $scope.save = function () {
      return $scope.editPartForm.$validate()
        .then(function () {
          if ($scope.editPartForm.$valid) {
            return AircraftParts
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftPart)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.parts.search');
              });
          }
        });
    };

    $scope.deletePart = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftParts.remove({ id: $stateParams.id, ind: aircraftPart.partIndex })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.parts.search');
          });
        }
      });
    };
  }

  PartsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftParts',
    'aircraftPart',
    'scMessage'
  ];

  PartsEditCtrl.$resolve = {
    aircraftPart: [
      '$stateParams',
      'AircraftParts',
      function ($stateParams, AircraftParts) {
        return AircraftParts.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PartsEditCtrl', PartsEditCtrl);
}(angular));
