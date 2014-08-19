/*global angular*/
(function (angular) {
  'use strict';

  function PartsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    parts
  ) {
    $scope.parts = parts;
  }

  PartsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'parts'
  ];

  PartsSearchCtrl.$resolve = {
    parts: [
      '$stateParams',
      'AircraftParts',
      function ($stateParams, AircraftParts) {
        return AircraftParts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PartsSearchCtrl', PartsSearchCtrl);
}(angular));
