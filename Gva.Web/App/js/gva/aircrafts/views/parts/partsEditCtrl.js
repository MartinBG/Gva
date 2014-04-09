/*global angular,_*/
(function (angular) {
  'use strict';

  function PartsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftPart,
    aircraftPart
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
            return AircraftPart
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftPart)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.parts.search');
              });
          }
        });
    };

    $scope.deletePart = function () {
      return AircraftPart.remove({ id: $stateParams.id, ind: aircraftPart.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.parts.search');
        });
    };
  }

  PartsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftPart',
    'aircraftPart'
  ];

  PartsEditCtrl.$resolve = {
    aircraftPart: [
      '$stateParams',
      'AircraftPart',
      function ($stateParams, AircraftPart) {
        return AircraftPart.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PartsEditCtrl', PartsEditCtrl);
}(angular));