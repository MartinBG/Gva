/*global angular*/
(function (angular) {
  'use strict';

  function AirportsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportInspection,
    airportInspection) {
    $scope.airportInspection = airportInspection;

    $scope.save = function () {
      return $scope.airportInspectionForm.$validate()
      .then(function () {
        if ($scope.airportInspectionForm.$valid) {
          return AirportInspection
            .save({
              id: $stateParams.id,
              ind: $stateParams.childInd ? $stateParams.childInd : $stateParams.ind
            }, $scope.airportInspection)
            .$promise
            .then(function () {
              return $stateParams.childInd ?
                $state.go('^') :
                $state.go('root.airports.view.inspections.search');
            });
        }
      });
    };


    $scope.cancel = function () {
      return $stateParams.childInd ?
        $state.go('^') :
        $state.go('root.airports.view.inspections.search');
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
          ind: $stateParams.childInd? $stateParams.childInd: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AirportsInspectionsEditCtrl', AirportsInspectionsEditCtrl);
}(angular));
