/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDataCtrl($scope, Aircrafts, scFormParams) {
    $scope.isUniqueMSN = function () {
      return Aircrafts.checkMSN({
        msn: $scope.model.manSN,
        aircraftId: scFormParams.lotId
      })
        .$promise
        .then(function (data) {
          return data.isValid;
        });
    };
  }

  AircraftDataCtrl.$inject = ['$scope', 'Aircrafts', 'scFormParams'];

  angular.module('gva').controller('AircraftDataCtrl', AircraftDataCtrl);
}(angular));
