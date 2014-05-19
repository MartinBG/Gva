/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessViewCtrl($scope, $state) {
    $scope.newAw = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
  }

  AircraftCertAirworthinessViewCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller(
    'AircraftCertAirworthinessViewCtrl',
    AircraftCertAirworthinessViewCtrl
    );
}(angular));
