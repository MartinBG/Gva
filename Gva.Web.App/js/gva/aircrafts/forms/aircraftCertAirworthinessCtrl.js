/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  AircraftCertAirworthinessCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftCertAirworthinessCtrl',
    AircraftCertAirworthinessCtrl);
}(angular));
