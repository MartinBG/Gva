/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataCtrl($scope, $stateParams, Aircraft) {
    $scope.isUniqueMSN = function (msn) {
      return Aircraft.checkMSN({ msn: msn, id: $stateParams.id }).$promise
        .then(function (data) {
          return data.isValid;
        });
    };
  }

  AircraftDataCtrl.$inject = [
    '$scope',
    '$stateParams',
    'Aircraft'
  ];

  angular.module('gva').controller('AircraftDataCtrl', AircraftDataCtrl);
}(angular));
