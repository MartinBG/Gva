/*global angular*/
(function (angular) {
  'use strict';

  function AircraftAirworthinessReviewCtrl($scope, scFormParams) {
    $scope.certType = scFormParams.certType;
    $scope.inspectorTypes = null;
    if ($scope.certType === '15a' || $scope.certType === '15b') {
      $scope.inspectorTypes = ['examiner','other'];
    } else {
      $scope.inspectorTypes = ['inspector', 'other'];
    }
  }

  AircraftAirworthinessReviewCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftAirworthinessReviewCtrl',
    AircraftAirworthinessReviewCtrl);
}(angular));
