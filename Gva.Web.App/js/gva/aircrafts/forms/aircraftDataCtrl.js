/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataCtrl($scope, $stateParams, Aircrafts) {
    $scope.isUniqueMSN = function (msn) {
      return Aircrafts.checkMSN({ msn: msn, id: $stateParams.id }).$promise
        .then(function (data) {
          return data.isValid;
        });
    };
  }

  AircraftDataCtrl.$inject = [
    '$scope',
    '$stateParams',
    'Aircrafts'
  ];

  angular.module('gva').controller('AircraftDataCtrl', AircraftDataCtrl);
}(angular));
