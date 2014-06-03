/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegFMCtrl($scope) {
    $scope.ownerIsOrg = true;
    $scope.operIsOrg = true;
    $scope.lessorIsOrg = true;

  }

  AircraftCertRegFMCtrl.$inject = ['$scope'];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
