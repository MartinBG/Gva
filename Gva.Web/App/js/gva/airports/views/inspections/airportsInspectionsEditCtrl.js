/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspection,
    airportInspection) {
    var originalDoc = _.cloneDeep(airportInspection);

    $scope.airportInspection = airportInspection;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return AirportInspection
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportInspection)
            .$promise
            .then(function () {
              return $state.go('root.airports.view.inspections.search');
            });
        }
      });
    };


    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportInspection = _.cloneDeep(originalDoc);
    };
    
    $scope.deleteInspection = function () {
      return AirportInspection.remove({
        id: $stateParams.id,
        ind: airportInspection.partIndex
      }).$promise.then(function () {
        return $state.go('root.airports.view.inspections.search');
      });
    };
  }

  AirportsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportInspection',
    'airportInspection'
  ];

  AirportsInspectionsEditCtrl.$resolve = {
    airportInspection: [
      '$stateParams',
      'AirportInspection',
      function ($stateParams, AirportInspection) {
        return AirportInspection.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportsInspectionsEditCtrl', AirportsInspectionsEditCtrl);
}(angular, _));
