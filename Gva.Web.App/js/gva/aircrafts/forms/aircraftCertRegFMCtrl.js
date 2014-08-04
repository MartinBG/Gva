/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegFMCtrl(
    $scope,
    gvaConstants
    ) {
    $scope.regMarkPattern = gvaConstants.regMarkPattern;
  }

  AircraftCertRegFMCtrl.$inject = [
    '$scope',
    'gvaConstants'
  ];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
