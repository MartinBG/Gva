/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataCtrl($scope, Aircrafts, scFormParams) {
    $scope.isUniqueMSN = function (msn) {
      return Aircrafts.checkMSN({ msn: msn, aircraftId: scFormParams.lotId }).$promise
        .then(function (data) {
          return data.isValid;
        });
    };
  }

  AircraftDataCtrl.$inject = ['$scope', 'Aircrafts', 'scFormParams'];

  angular.module('gva').controller('AircraftDataCtrl', AircraftDataCtrl);
}(angular));
