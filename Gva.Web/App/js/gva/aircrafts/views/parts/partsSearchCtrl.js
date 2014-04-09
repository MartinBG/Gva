/*global angular*/
(function (angular) {
  'use strict';

  function PartsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftPart,
    parts
  ) {
    $scope.parts = parts;

    $scope.editPart = function (part) {
      return $state.go('root.aircrafts.view.parts.edit',
        {
          id: $stateParams.id,
          ind: part.partIndex
        });
    };

    $scope.newPart = function () {
      return $state.go('root.aircrafts.view.parts.new');
    };
  }

  PartsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftPart',
    'parts'
  ];

  PartsSearchCtrl.$resolve = {
    parts: [
      '$stateParams',
      'AircraftPart',
      function ($stateParams, AircraftPart) {
        return AircraftPart.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PartsSearchCtrl', PartsSearchCtrl);
}(angular));
