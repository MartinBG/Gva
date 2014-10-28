/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessFMCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  AircraftCertAirworthinessFMCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftCertAirworthinessFMCtrl',
    AircraftCertAirworthinessFMCtrl);
}(angular));
