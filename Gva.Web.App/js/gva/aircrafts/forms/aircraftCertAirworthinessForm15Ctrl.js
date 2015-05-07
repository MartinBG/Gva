/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessForm15Ctrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  AircraftCertAirworthinessForm15Ctrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftCertAirworthinessForm15Ctrl',
    AircraftCertAirworthinessForm15Ctrl);
}(angular));
