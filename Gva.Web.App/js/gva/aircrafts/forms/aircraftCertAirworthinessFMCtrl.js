/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessFmCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  AircraftCertAirworthinessFmCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller(
    'AircraftCertAirworthinessFmCtrl',
    AircraftCertAirworthinessFmCtrl
    );
}(angular));
