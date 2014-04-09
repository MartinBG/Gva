/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftInspection,
    aircraftInspection) {
    var originalInspection = _.cloneDeep(aircraftInspection);

    $scope.aircraftInspection = aircraftInspection;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftInspection.part = _.cloneDeep(originalInspection.part);
      $scope.$broadcast('cancel', originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return AircraftInspection
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftInspection)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.inspections.search');
            });
        }
      });
    };

    $scope.deleteInspection = function () {
      return AircraftInspection.remove({
        id: $stateParams.id,
        ind: aircraftInspection.partIndex
      }).$promise.then(function () {
        return $state.go('root.aircrafts.view.inspections.search');
      });
    };
  }

  AircraftsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftInspection',
    'aircraftInspection'
  ];

  AircraftsInspectionsEditCtrl.$resolve = {
    aircraftInspection: [
      '$stateParams',
      'AircraftInspection',
      function ($stateParams, AircraftInspection) {
        return AircraftInspection.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsInspectionsEditCtrl', AircraftsInspectionsEditCtrl);
}(angular));
