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

    $scope.deletePart = function (part) {
      return AircraftPart.remove({ id: $stateParams.id, ind: part.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
